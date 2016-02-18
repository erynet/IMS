using System;
using System.Collections.Generic;
using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Server.Sub.WCFHost.Implement
{
    public partial class Contract
    {
        public IMSSetting GetSetting(string key)
        {
            throw new NotImplementedException();
        }

        public List<IMSSetting> GetAllSettings()
        {
            throw new NotImplementedException();
        }

        public bool SetSetting(IMSSetting value)
        {
            throw new NotImplementedException();
        }
    }
}