using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Transactions;
using IMS.Server.Sub.Lib.LocalDB;
using IMS.Server.Sub.Lib.LocalDB.Model;
using IMS.Server.Sub.WCFHost.Abstract;

namespace IMS.Server.Sub.WCFHost
{
    /*
    //Usage Scenario
    string guid = Proxy.Athenticate("root", "p@aSW0rd", "12-34-56-78-90-qw-er-ty");
    if (guid == "")
        ExitProgram("Athentication Failed");
    while(ContinueLoop)
    {
        Proxy.GetEvents(guid);
        Proxy.GetWarnings(guid);
        Proxy.GetAllDeviceStatus();
    }
    Proxy.Leave(guid);
    */
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Contract : IIMS, IDisposable
    {
        private OperationContext _operationContext;
        private InstanceContext _instanceContext;

        private readonly object _sessionLock;
        private readonly Dictionary<string, Sessioninfo> _sessions;

        public Contract()
        {
            //_operationContext = OperationContext.Current;
            //_instanceContext = _operationContext.InstanceContext;

            _sessionLock = new object();
            _sessions = new Dictionary<string, Sessioninfo>();

            using (var ctx = new LocalDB())
            {
                ctx.Database.Initialize(true);
            }
        }

        public string Athenticate(string id, string passwd, string macAddress)
        {
            Sessioninfo existSession = GetExistSession();
            if (existSession != null)
                return existSession.Session;

            int state = OpenSession(id, passwd, macAddress);

            if (state == 1)
            {
                // 성공적으로 세션이 생성되었음
                _operationContext = OperationContext.Current;
                string sessionId = _operationContext.SessionId;

                Debug.Assert(sessionId != null, "Athenticate : SessionId is null");
                Debug.Assert(_sessions[sessionId].Session == sessionId, "Athenticate : _sessions[sessionId].Session != sessionId");

                return sessionId;
            }
            if (state == 0)
            {
                // 세션 생성중 에러가 발생함.
                return "";

            }
            if (state == 2)
            {
                // 해당 유저가 존재하지 않음.
                return "";

            }
            if (state == 3)
            {
                // 해당 유저의 암호가 잂치하지 않음.
                return "";

            }
            Debug.Assert(false, $@"Athenticate : State == {state}");
            return "";
        }

        private string GetSessionId()
        {
            return OperationContext.Current.SessionId;
        }

        private Sessioninfo GetExistSession()
        {
            // 사용자가 같은 세션으로 이미 등록되어 있을수도 있으므로, 그것을 얻기 위한 함수를 구현한다.
            _operationContext = OperationContext.Current;
            string sessionId = _operationContext.SessionId;

            Debug.Assert(sessionId != null, "GetExistSession : SessionId is null");

            try
            {
                lock (_sessionLock)
                {
                    if(!_sessions.ContainsKey(sessionId))
                    {
                        using (var ctx = new LocalDB()) // DB 에서 세션을 꺼내기 위해 Scope 를 개방한다.
                        {
                            var q = from s in ctx.Session
                                where (s.Token == sessionId && s.IsClosed == false)
                                orderby s.SignIn descending
                                select s;

                            if (q.Any())    // DB 에서 해당 세션을 찾았다면.
                            {
                                Session existSession = q.First();   // 위의 조건에 부합하는것중 최신의 것을 하나 꺼낸다.
                                
                                // 접근한 클라이언트의 IP 를 추출하기 위한 루틴
                                MessageProperties prop = _operationContext.IncomingMessageProperties;
                                RemoteEndpointMessageProperty endpoint =
                                    prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                                var remoteAddress = endpoint != null ? endpoint.Address : "0.0.0.0";
                                
                                // DB 에 존재하는 세션정보를 기반으로 SessionInfo 객체를 하나 만들어서 목록에 추가한다.
                                Sessioninfo restoredSessionInfo = new Sessioninfo(session: sessionId,
                                    isAdmin: existSession.IsAdmin, sessionIdx: existSession.Idx,
                                    userIdx: existSession.UserIdx, userIp: remoteAddress,
                                    userMac: existSession.MacAddress);

                                _sessions.Add(sessionId, restoredSessionInfo);
                                return restoredSessionInfo;
                            }
                            return null;    // 해당 세션이 없다면 null 리턴한다.
                        }
                    }
                    return _sessions[sessionId];   // 세면 목록에서 해당 세션을 찾아서 리턴한다.
                }
            }
            catch (Exception)
            {
                return null;    // 예외가 발생해도 null 을 리턴한다.
            }
        }

        private int OpenSession(string id, string passwd, string macAddress)
        {
            // 새로운 세션을 연다. 이 함수는 Athenticate 에서만 사용되어야 한다.
            _operationContext = OperationContext.Current;
            string sessionId = _operationContext.SessionId;

            Debug.Assert(sessionId != null, "OpenSession : SessionId is null");

            try
            {
                using (var ctx = new LocalDB())
                {
                    var q = from u in ctx.User where (u.Id == id) select u;
                    if (q.Any())
                    {
                        User user = q.First();
                        if (!user.Authenticate(id, passwd))
                            return 3;    // Code 3: 암호가 일치하지 않는다.
                        Session newSession = new Session();
                        newSession.Enter(token: sessionId, macAddress: macAddress, userIdx: user.Idx,
                            isAdmin: user.IsAdmin);

                        MessageProperties prop = _operationContext.IncomingMessageProperties;
                        RemoteEndpointMessageProperty endpoint =
                            prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                        var remoteAddress = endpoint != null ? endpoint.Address : "0.0.0.0";
                        // DB 에 존재하는 세션정보를 기반으로 SessionInfo 객체를 하나 만들어서 목록에 추가한다.
                        Sessioninfo newSessionInfo = new Sessioninfo(session: sessionId,
                            isAdmin: newSession.IsAdmin, sessionIdx: newSession.Idx,
                            userIdx: newSession.UserIdx, userIp: remoteAddress,
                            userMac: newSession.MacAddress);

                        using (var trx = new TransactionScope())
                        {
                            ctx.Session.Add(newSession);
                            ctx.SaveChanges();
                            lock (_sessionLock)
                                _sessions.Add(sessionId, newSessionInfo);

                            trx.Complete();
                        }
                        return 1;   // Code 1: 정상적으로 새 세션이 생성되었다.
                    }
                    return 2;   // Code 2: 유저가 존재하지 않는다.
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return 0;    // Code 0: 에러가 발생하여 세션을 생성하지 못했다.
            }
        }

        private bool CloseSession()
        {
            // 세션을 닫는다. 이 함수는 Leave 에서만 사용되어야 한다.
            _operationContext = OperationContext.Current;
            string sessionId = _operationContext.SessionId;

            Debug.Assert(sessionId != null, "CloseSession : SessionId is null");

            lock (_sessionLock)
            {
                if (!_sessions.ContainsKey(sessionId))
                    return false;
                try
                {
                    using (var ctx = new LocalDB())
                    {
                        var q = from s in ctx.Session where (s.Token == sessionId) select s;
                        if (q.Any())
                        {
                            Session session = q.First();
                            session.Exit();

                            using (var trx = new TransactionScope())
                            {
                                ctx.SaveChanges();
                                _sessions.Remove(sessionId);

                                trx.Complete();
                            }
                            return true;
                        }
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        
        public bool Leave()
        {
            return CloseSession();      
        }

        public List<IMSEvent> GetEvents()
        {
            Sessioninfo sessioninfo = GetExistSession();
            if (sessioninfo == null)
                return null;

            using (var ctx = new LocalDB())
            {
                List<IMSEvent> result;
                try
                {
                    if (sessioninfo.EventIdx == 0)
                    {
                        var q = (from e in ctx.EventLog orderby e.TimeStamp descending
                                select new IMSEvent() { Idx = e.Idx, Description = e.Description, Code = e.Code, Data = e.Data}).Take(128);
                        if (q.Any())
                            result = q.ToList();
                        else
                            return null;
                    }
                    else
                    {
                        var q = (from e in ctx.EventLog where e.Idx > sessioninfo.EventIdx orderby e.TimeStamp descending
                                select new IMSEvent() { Idx = e.Idx, Description = e.Description, Code = e.Code, Data = e.Data }).Take(128);
                        if (q.Any())
                        {
                            result = q.ToList();
                        }
                        else
                            return null;
                    }

                    int maxEventId = result.Max(l => l.Idx);

                    using (var trx = new TransactionScope())
                    {
                        var q = from s in ctx.Session where s.Idx == sessioninfo.SessionIdx select s;
                        if (q.Any())
                        {
                            var sessionInDb = q.First();
                            sessionInDb.EventIdx = maxEventId;

                            ctx.SaveChanges();
                            sessioninfo.EventIdx = maxEventId;

                            lock (_sessionLock)
                                Debug.Assert(sessioninfo.EventIdx != _sessions[GetSessionId()].EventIdx);

                            trx.Complete();
                        }
                        else
                        {
                            Debug.Assert(false, "GetEvent : sessionInfo is exists in memory, but not exists in database");
                            return null;
                        }
                    }
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<IMSWarning> GetWarnings()
        {
            /*
            Sessioninfo sessioninfo = GetExistSession();
            if (sessioninfo == null)
                return null;

            using (var ctx = new LocalDB())
            {
                List<IMSWarning> result;
                try
                {
                    if (sessioninfo.EventIdx == 0)
                    {
                        var q = (from e in ctx.
                                 orderby e.TimeStamp descending
                                 select new IMSWarning() { Idx = e.Idx, Description = e.Description, Code = e.Code, Data = e.Data }).Take(128);
                        if (q.Any())
                            result = q.ToList();
                        else
                            return null;
                    }
                    else
                    {
                        var q = (from e in ctx.EventLog
                                 where e.Idx > sessioninfo.EventIdx
                                 orderby e.TimeStamp descending
                                 select new IMSWarning() { Idx = e.Idx, Description = e.Description, Code = e.Code, Data = e.Data }).Take(128);
                        if (q.Any())
                        {
                            result = q.ToList();
                        }
                        else
                            return null;
                    }

                    int maxEventId = result.Max(l => l.Idx);

                    using (var trx = new TransactionScope())
                    {
                        var q = from s in ctx.Session where s.Idx == sessioninfo.SessionIdx select s;
                        if (q.Any())
                        {
                            var sessionInDb = q.First();
                            sessionInDb.EventIdx = maxEventId;

                            ctx.SaveChanges();
                            sessioninfo.EventIdx = maxEventId;

                            lock (_sessionLock)
                                Debug.Assert(sessioninfo.EventIdx != _sessions[GetSessionId()].EventIdx);

                            trx.Complete();
                        }
                        else
                        {
                            Debug.Assert(false, "GetEvent : sessionInfo is exists in memory, but not exists in database");
                            return null;
                        }
                    }
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            */
            throw new NotImplementedException();
        }

        public IMSDeviceStatus GetDeviceStatus(int deviceId)
        {
            throw new NotImplementedException();
        }

        public List<IMSDeviceStatus> GetAllDeviceStatus()
        {
            throw new NotImplementedException();
        }

        public IMSSetting GetSetting(string key)
        {
            throw new NotImplementedException();
        }

        public Abstract.IMSSetting GetSetting()
        {
            throw new NotImplementedException();
        }

        public List<Abstract.IMSSetting> GetAllSettings()
        {
            throw new NotImplementedException();
        }

        public bool SetSetting(Abstract.IMSSetting value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            //throw new NotImplementedException();
        }
    }

    class Sessioninfo
    {
        public string Session { get; }
        public int EventIdx { get; set; }
        public int WarningIdx { get; set; }
        public bool IsAdmin { get; }
        public int SessionIdx { get; }
        public int UserIdx { get; }
        public string UserIp { get; }
        public string UserMac { get; }

        public Sessioninfo(string session, bool isAdmin, int sessionIdx, int userIdx, string userIp, string userMac)
        {
            this.Session = session;
            EventIdx = 0;
            WarningIdx = 0;
            IsAdmin = isAdmin;
            SessionIdx = sessionIdx;
            UserIdx = userIdx;
            UserIp = userIp;
            UserMac = userMac;
        }
    }
}