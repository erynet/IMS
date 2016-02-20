using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public partial class LocalDBDriver
    {
        #region Cdu
        public static List<Cdu.Info> GetCdus(bool ascending = true)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var upsTotal = (from u in ctx.Ups orderby u.Idx ascending select u).ToList();

                    if (ascending)
                    {
                        var cdus = (from c in ctx.Cdu orderby c.No ascending select c).ToList();
                        List<Cdu.Info> result = new List<Cdu.Info>();
                        foreach (var c in cdus)
                        {
                            result.Add(new Cdu.Info()
                            {
                                isUsing = c.Enabled,
                                cduIdx = c.Idx,
                                cduNo = c.No,
                                cduName = c.Name,
                                isExtended = c.Extendable,
                                //upsIdxList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.Idx).ToArray()),
                                //upsNoList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.No).ToArray())
                                //upsIdxList = new IntList((from u in upsTotal where Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                                //upsNoList = new IntList(Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                                upsIdxList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.Idx).ToArray()),
                                upsNoList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.No).ToArray()),
                                installDate = c.InstallAt,
                                ip = c.IpAddress
                            });
                        }
                        return result;
                    }
                        //return (from c in ctx.Cdu
                        //        orderby c.No ascending
                        //        select new Cdu.Info()
                        //        {
                        //            isUsing = c.Enabled,
                        //            cduIdx = c.Idx,
                        //            upsNo = c.No,
                        //            cduName = c.Name,
                        //            isExtended = c.Extendable,
                        //            //upsIdxList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.Idx).ToArray()),
                        //            //upsNoList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.No).ToArray())
                        //            //upsIdxList = new IntList((from u in upsTotal where Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                        //            //upsNoList = new IntList(Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                        //            upsIdxList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.Idx).ToArray()),
                        //            upsNoList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.No).ToArray()),
                        //            installDate = c.InstallAt,
                        //            ip = c.IpAddress
                        //        }).ToList();
                    else
                    {
                        var cdus = (from c in ctx.Cdu orderby c.No descending select c).ToList();
                        List<Cdu.Info> result = new List<Cdu.Info>();
                        foreach (var c in cdus)
                        {
                            result.Add(new Cdu.Info()
                            {
                                isUsing = c.Enabled,
                                cduIdx = c.Idx,
                                cduNo = c.No,
                                cduName = c.Name,
                                isExtended = c.Extendable,
                                //upsIdxList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.Idx).ToArray()),
                                //upsNoList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.No).ToArray())
                                //upsIdxList = new IntList((from u in upsTotal where Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                                //upsNoList = new IntList(Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                                upsIdxList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.Idx).ToArray()),
                                upsNoList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.No).ToArray()),
                                installDate = c.InstallAt,
                                ip = c.IpAddress
                            });
                        }
                        return result;
                    }
                    //return (from c in ctx.Cdu
                    //        orderby c.No descending
                    //        select new Cdu.Info()
                    //        {
                    //            isUsing = c.Enabled,
                    //            cduIdx = c.Idx,
                    //            upsNo = c.No,
                    //            cduName = c.Name,
                    //            isExtended = c.Extendable,
                    //            //upsIdxList = new IntList((from u in upsTotal where Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                    //            //upsNoList = new IntList(Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                    //            upsIdxList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.Idx).ToArray()),
                    //            upsNoList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.No).ToArray()),
                    //            installDate = c.InstallAt,
                    //            ip = c.IpAddress
                    //        }).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetCdus : {e.ToString()}");
                return new List<Cdu.Info>();
            }
        }

        public static Cdu.Info GetCduByIdx(int cduIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var upsTotal = (from u in ctx.Ups orderby u.Idx ascending select u).ToList();

                    var c = (from cdu in ctx.Cdu where cdu.Idx == cduIdx select cdu).DefaultIfEmpty(null).First();
                    if (c == null)
                        return null;

                    return new Cdu.Info()
                    {
                        isUsing = c.Enabled,
                        cduIdx = c.Idx,
                        cduNo = c.No,
                        cduName = c.Name,
                        isExtended = c.Extendable,
                        upsIdxList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.Idx).ToArray()),
                        upsNoList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.No).ToArray()),
                        installDate = c.InstallAt,
                        ip = c.IpAddress
                    };

                    //return (from c in ctx.Cdu
                    //        where c.Idx == cduIdx
                    //        select new Cdu.Info()
                    //        {
                    //            isUsing = c.Enabled,
                    //            cduIdx = c.Idx,
                    //            cduNo = c.No,
                    //            cduName = c.Name,
                    //            isExtended = c.Extendable,
                    //            //upsIdxList = new IntList((from u in upsTotal where Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                    //            //upsNoList = new IntList(Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                    //            upsIdxList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.Idx).ToArray()),
                    //            upsNoList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.No).ToArray()),
                    //            installDate = c.InstallAt,
                    //            ip = c.IpAddress
                    //        }).DefaultIfEmpty(null).First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetCduByIdx : {e.ToString()}");
                return null;
            }
        }

        public static Cdu.Info GetCduByNo(int cduNo)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var upsTotal = (from u in ctx.Ups orderby u.Idx ascending select u).ToList();

                    var c = (from cdu in ctx.Cdu where cdu.No == cduNo select cdu).DefaultIfEmpty(null).First();
                    if (c == null)
                        return null;

                    return new Cdu.Info()
                    {
                        isUsing = c.Enabled,
                        cduIdx = c.Idx,
                        cduNo = c.No,
                        cduName = c.Name,
                        isExtended = c.Extendable,
                        upsIdxList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.Idx).ToArray()),
                        upsNoList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.No).ToArray()),
                        installDate = c.InstallAt,
                        ip = c.IpAddress
                    };

                    //return (from c in ctx.Cdu
                    //        where c.No == cduNo
                    //        select new Cdu.Info()
                    //        {
                    //            isUsing = c.Enabled,
                    //            cduIdx = c.Idx,
                    //            cduNo = c.No,
                    //            cduName = c.Name,
                    //            isExtended = c.Extendable,
                    //            //upsIdxList = new IntList((from u in upsTotal where Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                    //            //upsNoList = new IntList(Regex.Split(c.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray()),
                    //            upsIdxList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.Idx).ToArray()),
                    //            upsNoList = new IntList((from u in upsTotal where u.CduNo == c.No orderby u.No ascending select u.No).ToArray()),
                    //            installDate = c.InstallAt,
                    //            ip = c.IpAddress
                    //        }).DefaultIfEmpty(null).First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetCduByNo : {e.ToString()}");
                return null;
            }
        }

        public static bool SetCdu(Cdu.Info cdu)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existCdu =
                        (from c in ctx.Cdu where c.Idx == cdu.cduIdx select c).DefaultIfEmpty(null).First();
                    if (existCdu == null)
                        return false;

                    //existCdu.GroupNo = cdu.
                    existCdu.No = cdu.cduNo;
                    existCdu.Name = cdu.cduName;
                    existCdu.Extendable = cdu.isExtended;
                    existCdu.IpAddress = cdu.ip;
                    existCdu.Enabled = cdu.isUsing;
                    existCdu.InstallAt = cdu.installDate;
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
                Console.WriteLine($"SetCdu : {e.ToString()}");
                return false;
            }
        }

        public static bool AddCdu(Cdu.Info cdu)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    Database.LocalDB.Model.CDU newcdu = new Database.LocalDB.Model.CDU()
                    {
                        No = cdu.cduNo,
                        Name = cdu.cduName,
                        Extendable = cdu.isExtended,
                        IpAddress = cdu.ip,
                        Enabled = cdu.isUsing,
                        InstallAt = cdu.installDate,
                    };

                    using (var trx = new TransactionScope())
                    {
                        ctx.Cdu.Add(newcdu);
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"AddCdu : {e.ToString()}");
                return false;
            }
        }

        public static bool DelCdu(int cduIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existCdu = (from c in ctx.Cdu where c.Idx == cduIdx select c).DefaultIfEmpty(null).First();
                    if (existCdu == null)
                        return false;

                    using (var trx = new TransactionScope())
                    {
                        ctx.Cdu.Remove(existCdu);
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"DelCdu : {e.ToString()}");
                return false;
            }
        }

        #endregion
    }
}