using System;
using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Server.Sub.WCFHost.Implement
{
    public partial class Contract
    {
        public IMSGroups GetGroups()
        {
            throw new NotImplementedException();
        }

        public IMSGroup GetGroup(int groupIdx)
        {
            throw new NotImplementedException();
        }

        public bool SetGroup(IMSGroup group)
        {
            throw new NotImplementedException();
        }

        public int AddGroup(IMSGroup group)
        {
            throw new NotImplementedException();
        }

        public bool DelGroup(int groupIdx)
        {
            throw new NotImplementedException();
        }
    }
}