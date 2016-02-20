using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public partial class LocalDBDriver
    {
        #region Group
        public static List<Group.Info> GetGroups(bool ascending = true)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var upsTotal = (from u in ctx.Ups orderby u.Idx ascending select u).ToList();

                    if (ascending)
                    {
                        var grs = (from g in ctx.Group orderby g.No ascending select g).ToList();
                        //var grs = (from g in ctx.Group select g).First();
                        List<Group.Info> result = new List<Group.Info>();
                        foreach (var g in grs)
                        {
                            result.Add(new Group.Info()
                            {
                                isUsing = g.Enabled,
                                groupIdx = g.Idx,
                                groupNo = g.No,
                                isGroupVisible = g.Display,
                                groupName = g.Name,
                                coordinate = new Point(g.CoordX, g.CoordY),
                                //upsIdxList = new IntList((from u in upsTotal where Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                                //upsNoList = new IntList(Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray())
                                upsIdxList = new IntList((from u in upsTotal where u.GroupNo == g.No orderby u.No ascending select u.Idx).ToArray()),
                                upsNoList = new IntList((from u in upsTotal where u.GroupNo == g.No orderby u.No ascending select u.No).ToArray())
                            });
                        }
                        return result;
                    }
                    //return (from g in ctx.Group
                    //        orderby g.No ascending
                    //        select new Group.Info()
                    //        {
                    //            isUsing = g.Enabled,
                    //            groupIdx = g.Idx,
                    //            groupNo = g.No,
                    //            isGroupVisible = g.Display,
                    //            groupName = g.Name,
                    //            coordinate = new Point(g.CoordX, g.CoordY),
                    //            //upsIdxList = new IntList((from u in upsTotal where Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                    //            //upsNoList = new IntList(Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray())
                    //            upsIdxList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.Idx).ToArray()),
                    //            upsNoList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.No).ToArray())
                    //        }).ToList();
                    else
                    {
                        var grs = (from g in ctx.Group orderby g.No ascending select g).ToList();
                        List<Group.Info> result = new List<Group.Info>();
                        foreach (var g in grs)
                        {
                            result.Add(new Group.Info()
                            {
                                isUsing = g.Enabled,
                                groupIdx = g.Idx,
                                groupNo = g.No,
                                isGroupVisible = g.Display,
                                groupName = g.Name,
                                coordinate = new Point(g.CoordX, g.CoordY),
                                //upsIdxList = new IntList((from u in upsTotal where Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                                //upsNoList = new IntList(Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray())
                                upsIdxList = new IntList((from u in upsTotal where u.GroupNo == g.No orderby u.No ascending select u.Idx).ToArray()),
                                upsNoList = new IntList((from u in upsTotal where u.GroupNo == g.No orderby u.No ascending select u.No).ToArray())
                            });
                        }
                        return result;
                    }
                    //return (from g in ctx.Group
                    //        orderby g.No descending
                    //        select new Group.Info()
                    //        {
                    //            isUsing = g.Enabled,
                    //            groupIdx = g.Idx,
                    //            groupNo = g.No,
                    //            isGroupVisible = g.Display,
                    //            groupName = g.Name,
                    //            coordinate = new Point(g.CoordX, g.CoordY),
                    //            //upsIdxList = new IntList((from u in upsTotal where Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                    //            //upsNoList = new IntList(Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray())
                    //            upsIdxList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.Idx).ToArray()),
                    //            upsNoList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.No).ToArray())
                    //        }).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetGroups : {e.ToString()}");
                return new List<Group.Info>();
            }
        }

        public static Group.Info GetGroupByIdx(int groupIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var upsTotal = (from u in ctx.Ups orderby u.Idx ascending select u).ToList();

                    var gr = (from g in ctx.Group
                              where g.Idx == groupIdx
                              select g).DefaultIfEmpty(null).First();
                    if (gr == null)
                        return null;

                    return new Group.Info()
                    {
                        isUsing = gr.Enabled,
                        groupIdx = gr.Idx,
                        groupNo = gr.No,
                        isGroupVisible = gr.Display,
                        groupName = gr.Name,
                        coordinate = new Point(gr.CoordX, gr.CoordY),
                        //upsIdxList = new IntList((from u in upsTotal where Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                        //upsNoList = new IntList(Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray())
                        upsIdxList = new IntList((from u in upsTotal where u.GroupNo == gr.Idx orderby u.No ascending select u.Idx).ToArray()),
                        upsNoList = new IntList((from u in upsTotal where u.GroupNo == gr.Idx orderby u.No ascending select u.No).ToArray())
                    };

                    //return (from g in ctx.Group
                    //        where g.Idx == groupIdx
                    //        select new Group.Info()
                    //        {
                    //            isUsing = g.Enabled,
                    //            groupIdx = g.Idx,
                    //            groupNo = g.No,
                    //            isGroupVisible = g.Display,
                    //            groupName = g.Name,
                    //            coordinate = new Point(g.CoordX, g.CoordY),
                    //            //upsIdxList = new IntList((from u in upsTotal where Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                    //            //upsNoList = new IntList(Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray())
                    //            upsIdxList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.Idx).ToArray()),
                    //            upsNoList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.No).ToArray())
                    //        }).DefaultIfEmpty(null).First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetGroupByIdx : {e.ToString()}");
                return null;
            }
        }

        public static Group.Info GetGroupByNo(int groupNo)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var upsTotal = (from u in ctx.Ups orderby u.Idx ascending select u).ToList();

                    var gr = (from g in ctx.Group
                              where g.Idx == groupNo
                              select g).DefaultIfEmpty(null).First();
                    if (gr == null)
                        return null;

                    return new Group.Info()
                    {
                        isUsing = gr.Enabled,
                        groupIdx = gr.Idx,
                        groupNo = gr.No,
                        isGroupVisible = gr.Display,
                        groupName = gr.Name,
                        coordinate = new Point(gr.CoordX, gr.CoordY),
                        //upsIdxList = new IntList((from u in upsTotal where Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                        //upsNoList = new IntList(Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray())
                        upsIdxList = new IntList((from u in upsTotal where u.GroupNo == gr.Idx orderby u.No ascending select u.Idx).ToArray()),
                        upsNoList = new IntList((from u in upsTotal where u.GroupNo == gr.Idx orderby u.No ascending select u.No).ToArray())
                    };

                    //return (from g in ctx.Group
                    //        where g.No == groupNo
                    //        select new Group.Info()
                    //        {
                    //            isUsing = g.Enabled,
                    //            groupIdx = g.Idx,
                    //            groupNo = g.No,
                    //            isGroupVisible = g.Display,
                    //            groupName = g.Name,
                    //            coordinate = new Point(g.CoordX, g.CoordY),
                    //            //upsIdxList = new IntList((from u in upsTotal where Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToList().Contains(u.No) select u.Idx).ToArray()),
                    //            //upsNoList = new IntList(Regex.Split(g.UpsList, @"\D+").Select(n => Convert.ToInt32(n)).ToArray())
                    //            upsIdxList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.Idx).ToArray()),
                    //            upsNoList = new IntList((from u in upsTotal where u.GroupNo == g.Idx orderby u.No ascending select u.No).ToArray())
                    //        }).DefaultIfEmpty(null).First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetGroupByNo : {e.ToString()}");
                return null;
            }
        }

        public static bool SetGroup(Group.Info gr)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existGroup =
                        (from g in ctx.Group where g.Idx == gr.groupIdx select g).DefaultIfEmpty(null).First();
                    if (existGroup == null)
                        return false;

                    existGroup.No = gr.groupNo;
                    existGroup.Name = gr.groupName;
                    existGroup.Display = gr.isGroupVisible;
                    existGroup.CoordX = gr.coordinate.X;
                    existGroup.CoordY = gr.coordinate.Y;
                    existGroup.Enabled = gr.isUsing;

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
                Console.WriteLine($"SetGroup : {e.ToString()}");
                return false;
            }
        }

        public static bool AddGroup(Group.Info gr)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    Database.LocalDB.Model.Group newGroup = new Database.LocalDB.Model.Group()
                    {
                        No = gr.groupNo,
                        Name = gr.groupName,
                        Display = gr.isGroupVisible,
                        CoordX = gr.coordinate.X,
                        CoordY = gr.coordinate.Y,
                        Enabled = gr.isUsing
                    };

                    using (var trx = new TransactionScope())
                    {
                        ctx.Group.Add(newGroup);
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"AddGroup : {e.ToString()}");
                return false;
            }
        }

        public static bool DelGroup(int groupIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existGroup = (from g in ctx.Group where g.Idx == groupIdx select g).DefaultIfEmpty(null).First();
                    if (existGroup == null)
                        return false;

                    using (var trx = new TransactionScope())
                    {
                        ctx.Group.Remove(existGroup);
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"DelGroup : {e.ToString()}");
                return false;
            }
        }

        #endregion


    }
}