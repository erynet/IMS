using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using IMS.Server.Sub.Lib.LocalDB;
using IMS.Server.Sub.Lib.LocalDB.Model;
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
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existCud = (from c in ctx.Cdu where c.Idx == cdu.Idx select c).DefaultIfEmpty(null).First();
                    if (existCud == null)
                        return false;

                    if (cdu.GroupIdx != null)
                    {
                        var existGroup = (from g in ctx.Group where g.Idx == cdu.GroupIdx select g).DefaultIfEmpty(null).First();
                        if (existGroup == null)
                            return false;

                        List<int> cduNoList = Regex.Split(existGroup.CduList, @"\D+").Select(n => Convert.ToInt32(n)).ToList();
                        if (!cduNoList.Contains(cdu.GroupIdx.Value))
                        {
                            cduNoList.Add(cdu.GroupIdx.Value);
                            existGroup.CduList = string.Join(",",
                                (from cduNo in cduNoList orderby cduNo ascending select $"{cduNo}").ToArray());
                        }
                        existCud.GroupIdx = cdu.GroupIdx.Value;
                    }

                    existCud.No = cdu.No;
                    existCud.Name = cdu.Name;
                    existCud.Extendable = cdu.Extendable;
                    existCud.IpAddress = cdu.IpAddress;
                    existCud.Enabled = cdu.Enabled;
                    existCud.InstallAt = cdu.InstallAt;
                    existCud.Description = cdu.Description;

                    using (var trx = new TransactionScope())
                    {
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public int AddCdu(IMSCdu cdu)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    CDU newCdu = new CDU()
                    {
                        No = cdu.No,
                        Name = cdu.Name,
                        Extendable = cdu.Extendable,
                        IpAddress = cdu.IpAddress,
                        Enabled = cdu.Enabled,
                        InstallAt = cdu.InstallAt,
                        Description = cdu.Description
                    };

                    if (cdu.GroupIdx != null)
                    {
                        var existGroup = (from g in ctx.Group where g.Idx == cdu.GroupIdx select g).DefaultIfEmpty(null).First();
                        if (existGroup == null)
                            return -1;

                        List<int> cduNoList = Regex.Split(existGroup.CduList, @"\D+").Select(n => Convert.ToInt32(n)).ToList();
                        if (!cduNoList.Contains(cdu.GroupIdx.Value))
                        {
                            cduNoList.Add(cdu.GroupIdx.Value);
                            existGroup.CduList = string.Join(",",
                                (from cduNo in cduNoList orderby cduNo ascending select $"{cduNo}").ToArray());
                        }
                        newCdu.GroupIdx = cdu.GroupIdx.Value;
                    }

                    using (var trx = new TransactionScope())
                    {
                        ctx.Cdu.Add(newCdu);
                        ctx.SaveChanges();

                        trx.Complete();
                    }
                    return newCdu.Idx;
                }
            }
            catch (Exception)
            {
                return -2;
            }
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