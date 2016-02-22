using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using IMS.Database.LocalDB;
using IMS.Database.LocalDB.Model;

namespace IMS.Client.Core.Data.DB
{
    public partial class LocalDBDriver
    {
        #region Ups
        public static List<Ups.Info> GetUpss(bool ascending = true)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var groupTotal = (from g in ctx.Group orderby g.No ascending select g).ToArray();
                    var upsTotal = (from u in ctx.Ups orderby u.No ascending select u).ToArray();
                    var cduTotal = (from c in ctx.Cdu orderby c.No ascending select c).ToArray();

                    if (ascending)
                    {
                        var upss = (from u in ctx.Ups orderby u.No ascending select u).ToList();
                        List<Ups.Info> result = new List<Ups.Info>();
                        foreach (var u in upss)
                        {
                            IntList tempPartnerIdxList;
                            IntList tempPartnerNoList;
                            if ((u.MateList == null) || (u.MateList.Length == 0))
                            {
                                tempPartnerIdxList = new IntList();
                                tempPartnerNoList = new IntList();
                            }
                            else
                            {
                                tempPartnerIdxList = new IntList((from iu in upsTotal where (Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).Contains(iu.No)) orderby iu.No ascending select iu.Idx).ToArray());
                                tempPartnerNoList = new IntList(Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray());
                            }

                            result.Add(new Ups.Info()
                            {
                                isUsing = u.Enabled,
                                upsIdx = u.Idx,
                                upsNo = u.No,
                                groupIdx = (from g in groupTotal where g.No == u.GroupNo select g.Idx).DefaultIfEmpty(0)
                                        .FirstOrDefault(),
                                groupNo = u.GroupNo,
                                upsName = u.Name,
                                partnerIdxList = tempPartnerIdxList,
                                partnerNoList = tempPartnerNoList,
                                cduIdx =
                                    (from c in cduTotal where c.Idx == u.CduNo select c.Idx).DefaultIfEmpty(0)
                                        .FirstOrDefault(),
                                cduNo = u.CduNo,
                                batteryDescription = u.Specification,
                                batteryCapacity = u.Capacity,
                                ip = u.IpAddress,
                                installDate = u.InstallAt
                            });
                        }
                        return result;
                    }
                    //return (from u in ctx.Ups
                    //        orderby u.No ascending
                    //        select new Ups.Info()
                    //        {
                    //            isUsing = u.Enabled,
                    //            upsIdx = u.Idx,
                    //            upsNo = u.No,
                    //            groupIdx = u.GroupNo,
                    //            groupNo = (from g in groupTotal where g.Idx == u.GroupNo select g.No).DefaultIfEmpty(0).FirstOrDefault(),
                    //            upsName = u.Name,
                    //            partnerIdxList = new IntList((from iu in upsTotal where Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray().Contains(iu.No) orderby iu.No ascending select iu.Idx).ToArray()),
                    //            partnerNoList = new IntList(Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                    //            cduIdx = (from c in cduTotal where c.Idx == u.CduNo select c.Idx).DefaultIfEmpty(0).FirstOrDefault(),
                    //            upsNo = u.CduNo,
                    //            batteryDescription = u.Specification,
                    //            batteryCapacity = u.Capacity,
                    //            ip = u.IpAddress,
                    //            installDate = u.InstallAt
                    //        }).ToList();
                    else
                    {
                        var upss = (from u in ctx.Ups orderby u.No descending select u).ToList();
                        List<Ups.Info> result = new List<Ups.Info>();
                        foreach (var u in upss)
                        {
                            IntList tempPartnerIdxList;
                            IntList tempPartnerNoList;
                            if ((u.MateList == null) || (u.MateList.Length == 0))
                            {
                                tempPartnerIdxList = new IntList();
                                tempPartnerNoList = new IntList();
                            }
                            else
                            {
                                tempPartnerIdxList = new IntList((from iu in upsTotal where (Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).Contains(iu.No)) orderby iu.No ascending select iu.Idx).ToArray());
                                tempPartnerNoList = new IntList(Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray());
                            }

                            result.Add(new Ups.Info()
                            {
                                isUsing = u.Enabled,
                                upsIdx = u.Idx,
                                upsNo = u.No,
                                groupIdx = u.GroupNo,
                                groupNo =
                                    (from g in groupTotal where g.Idx == u.GroupNo select g.No).DefaultIfEmpty(0)
                                        .FirstOrDefault(),
                                upsName = u.Name,
                                partnerIdxList = tempPartnerIdxList,
                                partnerNoList = tempPartnerNoList,
                                cduIdx =
                                    (from c in cduTotal where c.Idx == u.CduNo select c.Idx).DefaultIfEmpty(0)
                                        .FirstOrDefault(),
                                cduNo = u.CduNo,
                                batteryDescription = u.Specification,
                                batteryCapacity = u.Capacity,
                                ip = u.IpAddress,
                                installDate = u.InstallAt
                            });
                        }
                        return result;
                    }
                    //return (from u in ctx.Ups
                    //        orderby u.No descending
                    //        select new Ups.Info()
                    //        {
                    //            isUsing = u.Enabled,
                    //            upsIdx = u.Idx,
                    //            upsNo = u.No,
                    //            groupIdx = u.GroupNo,
                    //            groupNo = (from g in groupTotal where g.Idx == u.GroupNo select g.No).DefaultIfEmpty(0).FirstOrDefault(),
                    //            upsName = u.Name,
                    //            partnerIdxList = new IntList((from iu in upsTotal where Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray().Contains(iu.No) orderby iu.No ascending select iu.Idx).ToArray()),
                    //            partnerNoList = new IntList(Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                    //            cduIdx = (from c in cduTotal where c.Idx == u.CduNo select c.Idx).DefaultIfEmpty(0).FirstOrDefault(),
                    //            upsNo = u.CduNo,
                    //            batteryDescription = u.Specification,
                    //            batteryCapacity = u.Capacity,
                    //            ip = u.IpAddress,
                    //            installDate = u.InstallAt
                    //        }).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetUpss : {e.ToString()}");
                return new List<Ups.Info>();
            }
        }

        public static Ups.Info GetUpsByIdx(int upsIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var groupTotal = (from g in ctx.Group orderby g.No ascending select g).ToArray();
                    var upsTotal = (from ups in ctx.Ups orderby ups.No ascending select ups).ToArray();
                    var cduTotal = (from c in ctx.Cdu orderby c.No ascending select c).ToArray();

                    var u = (from ups in ctx.Ups where ups.Idx == upsIdx select ups).DefaultIfEmpty(null).First();
                    if (u == null)
                        return null;

                    IntList tempPartnerIdxList;
                    IntList tempPartnerNoList;
                    if ((u.MateList == null) || (u.MateList.Length == 0))
                    {
                        tempPartnerIdxList = new IntList();
                        tempPartnerNoList = new IntList();
                    }
                    else
                    {
                        tempPartnerIdxList = new IntList((from iu in upsTotal where (Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).Contains(iu.No)) orderby iu.No ascending select iu.Idx).ToArray());
                        tempPartnerNoList = new IntList(Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray());
                    }

                    return new Ups.Info()
                    {
                        isUsing = u.Enabled,
                        upsIdx = u.Idx,
                        upsNo = u.No,
                        groupIdx = u.GroupNo,
                        groupNo = (from g in groupTotal where g.Idx == u.GroupNo select g.No).DefaultIfEmpty(0).FirstOrDefault(),
                        upsName = u.Name,
                        partnerIdxList = tempPartnerIdxList,
                        partnerNoList = tempPartnerNoList,
                        cduIdx = (from c in cduTotal where c.Idx == u.CduNo select c.Idx).DefaultIfEmpty(0).FirstOrDefault(),
                        cduNo = u.CduNo,
                        batteryDescription = u.Specification,
                        batteryCapacity = u.Capacity,
                        ip = u.IpAddress,
                        installDate = u.InstallAt
                    };

                    //return (from u in ctx.Ups
                    //        where u.Idx == upsIdx
                    //        select new Ups.Info()
                    //        {
                    //            isUsing = u.Enabled,
                    //            upsIdx = u.Idx,
                    //            upsNo = u.No,
                    //            groupIdx = u.GroupNo,
                    //            groupNo = (from g in groupTotal where g.Idx == u.GroupNo select g.No).DefaultIfEmpty(0).FirstOrDefault(),
                    //            upsName = u.Name,
                    //            partnerIdxList = new IntList((from iu in upsTotal where Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray().Contains(iu.No) orderby iu.No ascending select iu.Idx).ToArray()),
                    //            partnerNoList = new IntList(Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                    //            cduIdx = (from c in cduTotal where c.Idx == u.CduNo select c.Idx).DefaultIfEmpty(0).FirstOrDefault(),
                    //            cduNo = u.CduNo,
                    //            batteryDescription = u.Specification,
                    //            batteryCapacity = u.Capacity,
                    //            ip = u.IpAddress,
                    //            installDate = u.InstallAt
                    //        }).DefaultIfEmpty(null).First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetUpsByIdx : {e.ToString()}");
                return null;
            }
        }

        public static Ups.Info GetUpsByNo(int upsNo)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var groupTotal = (from g in ctx.Group orderby g.No ascending select g).ToArray();
                    var upsTotal = (from ups in ctx.Ups orderby ups.No ascending select ups).ToArray();
                    var cduTotal = (from c in ctx.Cdu orderby c.No ascending select c).ToArray();

                    var u = (from ups in ctx.Ups where ups.No == upsNo select ups).DefaultIfEmpty(null).First();
                    if (u == null)
                        return null;

                    IntList tempPartnerIdxList;
                    IntList tempPartnerNoList;
                    if ((u.MateList == null) || (u.MateList.Length == 0))
                    {
                        tempPartnerIdxList = new IntList();
                        tempPartnerNoList = new IntList();
                    }
                    else
                    {
                        tempPartnerIdxList = new IntList((from iu in upsTotal where (Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).Contains(iu.No)) orderby iu.No ascending select iu.Idx).ToArray());
                        tempPartnerNoList = new IntList(Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray());
                    }

                    return new Ups.Info()
                    {
                        isUsing = u.Enabled,
                        upsIdx = u.Idx,
                        upsNo = u.No,
                        groupIdx = u.GroupNo,
                        groupNo = (from g in groupTotal where g.Idx == u.GroupNo select g.No).DefaultIfEmpty(0).FirstOrDefault(),
                        upsName = u.Name,
                        partnerIdxList = tempPartnerIdxList,
                        partnerNoList = tempPartnerNoList,
                        cduIdx = (from c in cduTotal where c.Idx == u.CduNo select c.Idx).DefaultIfEmpty(0).FirstOrDefault(),
                        cduNo = u.CduNo,
                        batteryDescription = u.Specification,
                        batteryCapacity = u.Capacity,
                        ip = u.IpAddress,
                        installDate = u.InstallAt
                    };

                    //return (from u in ctx.Ups
                    //        where u.No == upsNo
                    //        select new Ups.Info()
                    //        {
                    //            isUsing = u.Enabled,
                    //            upsIdx = u.Idx,
                    //            upsNo = u.No,
                    //            groupIdx = (from g in groupTotal where g.No == u.GroupNo select g.Idx).DefaultIfEmpty(0).FirstOrDefault(),
                    //            groupNo = u.GroupNo,
                    //            upsName = u.Name,
                    //            partnerIdxList = new IntList((from iu in upsTotal where Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray().Contains(iu.No) orderby iu.No ascending select iu.Idx).ToArray()),
                    //            partnerNoList = new IntList(Regex.Split(u.MateList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                    //            cduIdx = (from c in cduTotal where c.Idx == u.CduNo select c.Idx).DefaultIfEmpty(0).FirstOrDefault(),
                    //            cduNo = u.CduNo,
                    //            batteryDescription = u.Specification,
                    //            batteryCapacity = u.Capacity,
                    //            ip = u.IpAddress,
                    //            installDate = u.InstallAt
                    //        }).DefaultIfEmpty(null).First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetUpsByNo : {e.ToString()}");
                return null;
            }
        }

        public static bool SetUps(Ups.Info ups)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existUps =
                        (from u in ctx.Ups where u.Idx == ups.upsIdx select u).DefaultIfEmpty(null).First();
                    if (existUps == null)
                        return false;

                    //existCdu.GroupNo = cdu.
                    existUps.GroupNo = ups.groupNo;
                    existUps.No = ups.upsNo;
                    existUps.Name = ups.upsName;
                    existUps.MateList = ups.partnerNoList.ToString();
                    existUps.CduNo = ups.cduNo;
                    existUps.Specification = ups.batteryDescription;
                    existUps.Capacity = ups.batteryCapacity;
                    existUps.IpAddress = ups.ip;
                    existUps.Enabled = ups.isUsing;
                    existUps.InstallAt = ups.installDate;
                    //existCdu.Description = cdu.

                    using (var trx = new TransactionScope())
                    {
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"SetUps : {e.ToString()}");
                return false;
            }
        }

        public static bool AddUps(Ups.Info ups)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    UPS newUps = new UPS()
                    {
                        GroupNo = ups.groupNo,
                        No = ups.upsNo,
                        Name = ups.upsName,
                        MateList = ups.partnerNoList.ToString(),
                        CduNo = ups.cduNo,
                        Specification = ups.batteryDescription,
                        Capacity = ups.batteryCapacity,
                        IpAddress = ups.ip,
                        Enabled = ups.isUsing,
                        InstallAt = ups.installDate,
                    };

                    using (var trx = new TransactionScope())
                    {
                        ctx.Ups.Add(newUps);
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"AddUps : {e.ToString()}");
                return false;
            }
        }

        public static bool DelUps(int upsIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existUps = (from u in ctx.Ups where u.Idx == upsIdx select u).DefaultIfEmpty(null).First();
                    if (existUps == null)
                        return false;

                    using (var trx = new TransactionScope())
                    {
                        ctx.Ups.Remove(existUps);
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"DelUps : {e.ToString()}");
                return false;
            }
        }

        #endregion
    }
}