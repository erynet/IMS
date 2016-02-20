using System;
using System.Collections.Generic;
using System.Linq;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public class LocalDBDriver
    {
        public List<Group.Info> GetGroups(bool ascending = true)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    if (ascending)
                        return (from g in ctx.Group orderby g.No ascending select new Group.Info()
                        { isUsing = g.Enabled, }).ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetGroups : {e.ToString()}");
                return new List<Group.Info>();
            }
        }

        public List<Ups> GetUpss(bool asending = true)
        {

        }

        public List<Cdu> GetCdus(bool asending = true)
        {

        }


    }
}