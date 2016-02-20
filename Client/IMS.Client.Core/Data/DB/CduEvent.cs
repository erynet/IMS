using System;
using System.Collections.Generic;
using System.Linq;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public partial class LocalDBDriver
    {
        public static List<CduEvent.Info> GetRecentCduEvent(int cduNo, int maxRow = 100)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var ces =
                        (from s in ctx.CduEvent where s.CduNo == cduNo orderby s.Idx descending select s).Take(maxRow).ToList();
                    List<CduEvent.Info> result = new List<CduEvent.Info>();
                    foreach (var ce in ces)
                    {
                        result.Add(new CduEvent.Info()
                        {
                            idx = ce.Idx,
                            cduNo = ce.CduNo,
                            title = ce.Title,
                            body = ce.Body,
                            priority = ce.Priority,
                            timeStamp = ce.TimeStamp
                        });
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetRecentCduEvent : {e.ToString()}");
                return new List<CduEvent.Info>();
            }
        }
    }
}