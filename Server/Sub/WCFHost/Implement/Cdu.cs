using System;
using System.Collections.Generic;
using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Server.Sub.WCFHost.Implement
{
    public partial class Contract
    {
        public List<IMSCdu> GetAllCdu(int groupIdx)
        {
            throw new NotImplementedException();
        }

        public IMSCdu GetCdu(int cduIdx)
        {
            throw new NotImplementedException();
        }

        public bool SetCdu(IMSCdu cdu)
        {
            throw new NotImplementedException();
        }

        public int AddCdu(IMSCdu cdu)
        {
            throw new NotImplementedException();
        }

        public bool DelCdu(int cduIdx)
        {
            throw new NotImplementedException();
        }

        public IMSCduStatus GetCduStatus(int cduIdx)
        {
            throw new NotImplementedException();
        }

        public List<IMSCduEvent> GetCduEvents(int cduIdx, int maxCount = 100, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?))
        {
            throw new NotImplementedException();
        }

        public List<IMSCduSocket> GetCduSocket(int cduIdx)
        {
            throw new NotImplementedException();
        }

        public bool SetCduSocket(IMSCduSocket cduSocket)
        {
            throw new NotImplementedException();
        }
    }
}