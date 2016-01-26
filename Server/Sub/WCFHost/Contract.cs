using System;
using System.ServiceModel;
using IMS.Server.Sub.Lib.LocalDB;
using IMS.Server.Sub.WCFHost.Abstract;

namespace IMS.Server.Sub.WCFHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Contract : IIMS, IDisposable
    {
        private LocalDB _lDb;

        public Contract()
        {
            _lDb = new LocalDB();
            _lDb.Database.Initialize(false);

        }

        public string Athenticate(string id, string passwd, string macAddress)
        {
            
            throw new NotImplementedException();
        }

        public string Leave(string guid)
        {
            throw new NotImplementedException();
        }

        public Event[] GetEvents(string guid)
        {
            throw new NotImplementedException();
        }

        public Warning[] GetWarnings(string guid)
        {
            throw new NotImplementedException();
        }

        public DeviceStatus GetDeviceStatus(int deviceId)
        {
            throw new NotImplementedException();
        }

        public DeviceStatus[] GetAllDeviceStatus()
        {
            throw new NotImplementedException();
        }

        public Setting GetSetting(string key)
        {
            throw new NotImplementedException();
        }

        public Setting[] GetAllSettings()
        {
            throw new NotImplementedException();
        }

        public bool SetSetting(string guid, Setting value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /*
        private int x = 0;

        private string _sessionId;
        private readonly OperationContext _operationContext;
        private readonly InstanceContext _instanceContext;


        public Contract()
        {
            _operationContext = OperationContext.Current;
            _instanceContext = _operationContext.InstanceContext;
            _sessionId = _operationContext.SessionId;
        }

        public string SessionOpen()
        {
            throw new NotImplementedException();
        }

        public bool AckImageUploadComplete(int imageId)
        {
            //throw new NotImplementedException();
        }

        public bool Athenticate(string uuid)
        {
            //throw new NotImplementedException();
            return true;
        }

        //public bool Ping()
        //{
        //    //throw new NotImplementedException();
        //    return true;
        //}

        public string Registration(Identity identity)
        {
            //throw new NotImplementedException();
            return "uuid";
        }

        public void ReportSessionSummary(DataContracts sessionSummary)
        {
            //throw new NotImplementedException();
        }

        public SiteInfo RequestOpenFtpSite()
        {
            throw new NotImplementedException();
        }


        public void SessionClose(bool isSuceess)
        {
            //throw new NotImplementedException();
        }

        public int Set(int x)
        {
            this.x = x;
            return this.x;
        }

        public int Get()
        {
            return x;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        */

    }
}