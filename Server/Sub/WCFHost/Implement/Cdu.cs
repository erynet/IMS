using System;
using System.Collections.Generic;
using System.Linq;
using IMS.Server.Sub.Lib.LocalDB;
using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Server.Sub.WCFHost.Implement
{
    public partial class Contract
    {
        public List<IMSCdu> GetAllCdu(int? groupIdx = null)
        {
            throw new NotImplementedException();
        }

        public IMSCdu GetCdu(int cduIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var cdu = (from c in ctx.Cdu where c.Idx == cduIdx select c).DefaultIfEmpty(null).First();
                    if (cdu == null)
                        return null;

                    IMSCdu result = new IMSCdu()
                    {
                        Idx = cdu.Idx,
                        GroupIdx = cdu.GroupIdx,
                        No = cdu.No,
                        Name = cdu.Name,
                        UpsList = cdu.UpsList,
                        Extendable = cdu.Extendable,
                        ContractCount = cdu.ContractCount,
                        IpAddress = cdu.IpAddress,
                        Status = cdu.Status,
                        Enabled = cdu.Enabled,
                        InstallAt = cdu.InstallAt,
                        Description = cdu.Description
                    };
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
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