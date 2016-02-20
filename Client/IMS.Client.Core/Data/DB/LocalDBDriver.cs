using System;
using System.Collections.Generic;
using System.Linq;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public class LocalDBDriver
    {
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
                                { isUsing = g.Enabled, }).ToList();
                    return (from g in ctx.Group
                        orderby g.No descending 
                        select new Group.Info()
                        { isUsing = g.Enabled, }).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetGroups : {e.ToString()}");
                return new List<Group.Info>();
            }
        }

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


    }
}