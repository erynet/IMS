using System;
using System.Collections.Concurrent;
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
using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Server.Sub.WCFHost.Implement
{
    /*
    //Usage Scenario
    string sessionId = Proxy.Athenticate("12-34-56-78-90-qw-er-ty");
    while(ContinueLoop)
    {
        List<IMSEvent> Events = Proxy.GetEvents();
        var grouped = from e in Events group e by e.Code select e;
        foreach (IMSEvent ie in grouped)
        {
            switch()
            {
                case 0:
                    // do nothing
                    break;
                case 10:
                    // have warning
                    var warn = Proxy.GetWarnings();
                    ...
                    break;
                case 100:
                    // ups status modificated
                    var uos = Proxy.GetAllUpsStatus();
                    ...
                    break;
                case 1000:
                    // global setting modified
                    var settings = Proxy.GetAllSettings();
                    ...
                    break;
            }
        }
    }
    Proxy.Leave(guid);
    */

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class Contract : IIMS, IDisposable
    {
        private OperationContext _operationContext;
        private InstanceContext _instanceContext;

        private readonly object _sessionLock;
        private readonly ConcurrentDictionary<string, IMSSession> _sessions;

        public Contract()
        {
            _sessionLock = new object();
            _sessions = new ConcurrentDictionary<string, IMSSession>();

            using (var ctx = new LocalDB())
            {
                ctx.Database.Initialize(true);
            }
        }

        public string Ping()
        {
            return "PONG";
        }

        public string Athenticate(string macAddress)
        {
            throw new NotImplementedException();
        }

        public bool Leave()
        {
            throw new NotImplementedException();
        }

        public List<IMSEvent> GetEvents(int maxCount = 100, int? priority = default(int?),
            DateTime? from = default(DateTime?), DateTime? to = default(DateTime?))
        {
            throw new NotImplementedException();
        }

        public List<IMSWarning> GetWarnings()
        {
            throw new NotImplementedException();
        }

        #region Support Function

        private IMSSession GetSession(string macAddress)
        {

            // 사용자가 같은 세션으로 이미 등록되어 있을수도 있으므로, 그것을 얻기 위한 함수를 구현한다.
            string sessionId = OperationContext.Current.SessionId;

            Debug.Assert(sessionId != null, "GetExistSession : SessionId is null");

            try
            {
                if (!_sessions.ContainsKey(sessionId))
                {
                    // 접근한 클라이언트의 IP 를 추출하기 위한 루틴
                    MessageProperties prop = _operationContext.IncomingMessageProperties;
                    RemoteEndpointMessageProperty endpoint =
                        prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    var remoteIpAddress = endpoint != null ? endpoint.Address : "0.0.0.0";

                    using (var ctx = new LocalDB()) // DB 에서 세션을 꺼내기 위해 Scope 를 개방한다.
                    {
                        var q = from s in ctx.Session where s.MacAddress == sessionId select s;

                        if (q.Any()) // DB 에서 해당 세션을 찾았다면.
                        {
                            Session existSession = q.First(); // 위의 조건에 부합하는것중 최신의 것을 하나 꺼낸다.

                            // DB 에 존재하는 세션정보를 기반으로 SessionInfo 객체를 하나 만들어서 목록에 추가한다.
                            IMSSession restoredSession = new IMSSession(sessionId, remoteIpAddress, macAddress);
                            restoredSession.EventIdx = existSession.EventIdx;
                            restoredSession.WarningIdx = existSession.WarningIdx;
                            
                            _sessions.TryAdd(sessionId, restoredSession);
                            return restoredSession;
                        }
                        else
                        {
                            int eventId = (from el in ctx.EventLog orderby el.Idx descending select el.Idx).
                                DefaultIfEmpty(0).First();
                            int warningId = (from wl in ctx.WarningLog orderby wl.Idx descending select wl.Idx).
                                DefaultIfEmpty(0).First();

                            // 이 지점.

                            IMSSession restoredSession = new IMSSession(sessionId, remoteIpAddress, macAddress);
                            restoredSession.EventIdx = eventId;
                            restoredSession.WarningIdx = warningId;

                            _sessions.TryAdd(sessionId, restoredSession);

                        }
                    }
                }
                return _sessions[sessionId]; // 세면 목록에서 해당 세션을 찾아서 리턴한다.
            }

            catch (Exception)
            {
                return null; // 예외가 발생해도 null 을 리턴한다.
            }
        }

        #endregion


        #region IDisposable Support

        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~Contract() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }

        #endregion

    }
    class IMSSession
    {
        public string SessionId { get; }
        public string MacAddress { get; }
        public string IpAddress { get; }
        public int EventIdx { get; set; }
        public int WarningIdx { get; set; }

        public IMSSession(string sessionId, string ipAddress, string macAddress)
        {
            SessionId = sessionId;
            IpAddress = ipAddress;
            MacAddress = macAddress;
            EventIdx = 0;
            WarningIdx = 0;
        }
    }


    /*
        public string Athenticate(string id, string passwd, string macAddress)
        {
            IMSSession existSession = GetExistSession();
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

        private IMSSession GetExistSession()
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
                                IMSSession restoredSessionInfo = new IMSSession(session: sessionId,
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
            
            throw new NotImplementedException();
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
                        IMSSession newSessionInfo = new IMSSession(session: sessionId,
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
            
            throw new NotImplementedException();
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
            
            throw new NotImplementedException();
        }
        
        public bool Leave()
        {
            return CloseSession();      
        }

        public List<IMSEvent> GetEvents()
        {
            IMSSession sessioninfo = GetExistSession();
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
            
            IMSSession sessioninfo = GetExistSession();
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
            
            throw new NotImplementedException();
        }
        */


}
