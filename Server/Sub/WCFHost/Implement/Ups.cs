using System;
using System.Collections.Generic;
using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Server.Sub.WCFHost.Implement
{
    public partial class Contract
    {
        public List<IMSUps> GetAllUps(int? groupIdx)
        {
            throw new NotImplementedException();
        }

        public IMSUps GetUps(int upsIdx)
        {
            throw new NotImplementedException();
        }

        public bool SetUps(IMSUps ups)
        {
            throw new NotImplementedException();
        }

        public int AddUps(IMSUps ups)
        {
            throw new NotImplementedException();
        }

        public bool DelUps(int upsIdx)
        {
            throw new NotImplementedException();
        }

        public IMSUpsStatus GetUpsStatus(int upsIdx)
        {
            throw new NotImplementedException();
        }

        public List<IMSUpsEvent> GetUpsEvents(int upsIdx, int maxCount = 100, DateTime? from = default(DateTime?), DateTime? to = default(DateTime?))
        {
            throw new NotImplementedException();
        }
    }
}