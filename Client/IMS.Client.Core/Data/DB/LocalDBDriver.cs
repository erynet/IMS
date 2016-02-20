﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public class LocalDBDriver
    {
        #region Group
        public static List<Group.Info> GetGroups(bool ascending = true)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    if (ascending)
                        return (from g in ctx.Group
                                orderby g.No ascending
                                select new Group.Info()
                                { }).ToList();
                    return (from g in ctx.Group
                            orderby g.No descending
                            select new Group.Info()
                            { }).ToList();
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
                    return (from g in ctx.Group
                            where g.Idx == groupIdx
                            select new Group.Info()
                            { }).DefaultIfEmpty(null).First();
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
                    return (from g in ctx.Group
                            where g.No == groupNo
                            select new Group.Info()
                            { }).DefaultIfEmpty(null).First();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetGroupByNo : {e.ToString()}");
                return null;
            }
        }

        public static bool SetGroup(Group.Info group)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    using (var trx = new TransactionScope())
                    {

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

        public static bool AddGroup(Group.Info group)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    using (var trx = new TransactionScope())
                    {

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
                    using (var trx = new TransactionScope())
                    {

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

        #region Ups
        public static List<Ups.Info> GetUpss(bool ascending = true)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    if (ascending)
                        return (from u in ctx.Ups
                                orderby u.No ascending
                                select new Ups.Info()).ToList();
                    return (from u in ctx.Ups
                            orderby u.No descending
                            select new Ups.Info()).ToList();
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
                    return (from u in ctx.Ups
                            where u.Idx == upsIdx
                            select new Ups.Info()
                            { }).DefaultIfEmpty(null).First();
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
                    return (from u in ctx.Ups
                            where u.No == upsNo
                            select new Ups.Info()
                            { }).DefaultIfEmpty(null).First();
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
                    using (var trx = new TransactionScope())
                    {

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
                    using (var trx = new TransactionScope())
                    {

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
                    using (var trx = new TransactionScope())
                    {

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

        #region Cdu
        public static List<Cdu.Info> GetCdus(bool ascending = true)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    if (ascending)
                        return (from c in ctx.Cdu
                                orderby c.No ascending
                                select new Cdu.Info()).ToList();
                    return (from c in ctx.Cdu
                            orderby c.No descending
                            select new Cdu.Info()).ToList();
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
                    return (from c in ctx.Cdu
                            where c.Idx == cduIdx
                            select new Cdu.Info()
                            { }).DefaultIfEmpty(null).First();
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
                    return (from c in ctx.Cdu
                            where c.No == cduNo
                            select new Cdu.Info()
                            { }).DefaultIfEmpty(null).First();
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
                    using (var trx = new TransactionScope())
                    {

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
                    using (var trx = new TransactionScope())
                    {

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
                    using (var trx = new TransactionScope())
                    {

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