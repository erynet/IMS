using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Transactions;
using IMS.Server.Sub.Lib.LocalDB;
using IMS.Server.Sub.Lib.LocalDB.Model;
using IMS.Server.Sub.WCFHost.Abstract.DataContract;
using Group = IMS.Server.Sub.Lib.LocalDB.Model.Group;

namespace IMS.Server.Sub.WCFHost.Implement
{
    public partial class Contract
    {
        public IMSGroups GetGroups()
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var groups = (from g in ctx.Group orderby g.No ascending select g).ToList();
                    if (!groups.Any())
                        return null;

                    List<IMSGroup> groupList = new List<IMSGroup>();
                    foreach (var group in groups)
                    {
                        int[] upsNoArray = Regex.Split(group.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray();
                        IMSUps[] upss = (from u in ctx.Ups
                            where upsNoArray.Contains(u.No)
                            select new IMSUps()
                            {
                                Idx = u.Idx,
                                GroupIdx = u.GroupIdx,
                                No = u.No,
                                Name = u.Name,
                                MateList = u.MateList,
                                CduNo = u.CduNo,
                                Specification = u.Specification,
                                Capacity = u.Capacity,
                                IpAddress = u.IpAddress,
                                Status = u.Status,
                                Enabled = u.Enabled,
                                InstallAt = u.InstallAt,
                                Description = u.Description
                            }).ToArray();

                        int[] cduNoArray = Regex.Split(group.CduList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray();
                        IMSCdu[] cdus = (from c in ctx.Cdu
                            where cduNoArray.Contains(c.No)
                            select new IMSCdu()
                            {
                                Idx = c.Idx,
                                GroupIdx = c.GroupIdx,
                                No = c.No,
                                Name = c.Name,
                                UpsList = c.UpsList,
                                Extendable = c.Extendable,
                                ContractCount = c.ContractCount,
                                IpAddress = c.IpAddress,
                                Status = c.Status,
                                Enabled = c.Enabled,
                                InstallAt = c.InstallAt,
                                Description = c.Description
                            }).ToArray();

                        IMSGroup imsGroup = new IMSGroup()
                        {
                            Idx = group.Idx,
                            No = group.No,
                            Name = group.Name,
                            Display = group.Display,
                            CoordX = group.CoordX,
                            CoordY = group.CoordY,
                            UpsList = upss.ToList(),
                            CduList = cdus.ToList(),
                            Status = group.Status,
                            Enabled = group.Enabled,
                            Description = group.Description
                        };
                        groupList.Add(imsGroup);
                    }

                    IMSGroups result = new IMSGroups()
                    {
                        Groups = groupList,
                        At = DateTime.Now
                    };

                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IMSGroup GetGroup(int groupIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var group = (from g in ctx.Group where g.Idx == groupIdx select g).DefaultIfEmpty(null).First();
                    if (group == null)
                        return null;

                    int[] upsNoArray = Regex.Split(group.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray();
                    IMSUps[] upss = (from u in ctx.Ups
                        where upsNoArray.Contains(u.No)
                        select new IMSUps()
                        {
                            Idx = u.Idx,
                            GroupIdx = u.GroupIdx,
                            No = u.No,
                            Name = u.Name,
                            MateList = u.MateList,
                            CduNo = u.CduNo,
                            Specification = u.Specification,
                            Capacity = u.Capacity,
                            IpAddress = u.IpAddress,
                            Status = u.Status,
                            Enabled = u.Enabled,
                            InstallAt = u.InstallAt,
                            Description = u.Description
                        }).ToArray();

                    int[] cduNoArray = Regex.Split(group.CduList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray();
                    IMSCdu[] cdus = (from c in ctx.Cdu
                        where cduNoArray.Contains(c.No)
                        select new IMSCdu()
                        {
                            Idx = c.Idx,
                            GroupIdx = c.GroupIdx,
                            No = c.No,
                            Name = c.Name,
                            UpsList = c.UpsList,
                            Extendable = c.Extendable,
                            ContractCount = c.ContractCount,
                            IpAddress = c.IpAddress,
                            Status = c.Status,
                            Enabled = c.Enabled,
                            InstallAt = c.InstallAt,
                            Description = c.Description
                        }).ToArray();

                    IMSGroup result = new IMSGroup()
                    {
                        Idx = group.Idx,
                        No = group.No,
                        Name = group.Name,
                        Display = group.Display,
                        CoordX = group.CoordX,
                        CoordY = group.CoordY,
                        UpsList = upss.ToList(),
                        CduList = cdus.ToList(),
                        Status = group.Status,
                        Enabled = group.Enabled,
                        Description = group.Description
                    };
                    return result;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool SetGroup(IMSGroup g)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existGroup = (from gr in ctx.Group where gr.Idx == g.Idx select gr).DefaultIfEmpty(null).First();
                    if (existGroup == null)
                        return false;

                    existGroup.No = g.No;
                    existGroup.Name = g.Name;
                    existGroup.Display = g.Display;
                    existGroup.CoordX = g.CoordX;
                    existGroup.CoordY = g.CoordY;
                    existGroup.UpsList =
                        string.Join(",",
                            (from upsNo in g.UpsList orderby upsNo ascending select $"{upsNo}").ToArray());
                    existGroup.CduList =
                        string.Join(",",
                            (from cudNo in g.CduList orderby cudNo ascending select $"{cudNo}").ToArray());
                    existGroup.Enabled = g.Enabled;
                    existGroup.Description = g.Description;

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

        public int AddGroup(IMSGroup g)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    Lib.LocalDB.Model.Group newGroup = new Group()
                    {
                        No = g.No,
                        Name = g.Name,
                        Display = g.Display,
                        CoordX = g.CoordX,
                        CoordY = g.CoordY,
                        UpsList =
                            string.Join(",",
                                (from upsNo in g.UpsList orderby upsNo ascending select $"{upsNo}").ToArray()),
                        CduList =
                            string.Join(",",
                                (from cudNo in g.CduList orderby cudNo ascending select $"{cudNo}").ToArray()),
                        Enabled = g.Enabled,
                        Description = g.Description
                    };
                    using (var trx = new TransactionScope())
                    {
                        ctx.Group.Add(newGroup);
                        ctx.SaveChanges();

                        trx.Complete();
                    }

                    return newGroup.Idx;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public bool DelGroup(int groupIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existGroup =
                        (from gr in ctx.Group where gr.Idx == groupIdx select gr).DefaultIfEmpty(null).First();
                    if (existGroup == null)
                        return false;

                    using (var trx = new TransactionScope())
                    {
                        ctx.Group.Attach(existGroup);
                        ctx.Group.Remove(existGroup);
                        ctx.SaveChanges();

                        trx.Complete();
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}