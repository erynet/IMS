using System;
using System.Collections.Generic;
using System.Linq;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public partial class LocalDBDriver
    {
        public static List<UpsEvent.Info> GetRecentUpsEvent(int upsNo, int maxRow = 100)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var ces =
                        (from s in ctx.UpsEvent where s.UpsNo == upsNo orderby s.Idx descending select s).Take(maxRow).ToList();
                    List<UpsEvent.Info> result = new List<UpsEvent.Info>();
                    foreach (var ce in ces)
                    {
                        result.Add(new UpsEvent.Info()
                        {
                            idx = ce.Idx,
                            upsNo = ce.UpsNo,
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
                Console.WriteLine($"GetRecentUpsEvent : {e.ToString()}");
                return new List<UpsEvent.Info>();
            }
        }

        public static List<UpsEvent.Info> GetUpsEventRange(int upsNo, DateTime from_time, DateTime to_time)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var ces =
                        (from s in ctx.UpsEvent where s.UpsNo == upsNo && (s.TimeStamp >= from_time && s.TimeStamp <= to_time) orderby s.Idx descending select s).ToList();
                    List<UpsEvent.Info> result = new List<UpsEvent.Info>();
                    foreach (var ce in ces)
                    {
                        result.Add(new UpsEvent.Info()
                        {
                            idx = ce.Idx,
                            upsNo = ce.UpsNo,
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
                Console.WriteLine($"GetUpsEventRange : {e.ToString()}");
                return new List<UpsEvent.Info>();
            }
        }
    }
}