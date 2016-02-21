using System;
using System.Collections.Generic;
using System.Linq;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public partial class LocalDBDriver
    {
        public static List<EventLog.Info> GetEventLog(DateTime fromTime)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var els = (from el in ctx.EventLog where el.TimeStamp >= fromTime orderby el.Idx descending select el).ToList();
                    List<EventLog.Info> result = new List<EventLog.Info>();
                    foreach (var el in els)
                    {
                        result.Add(new EventLog.Info()
                        {
                            idx = el.Idx,
                            code = el.Code,
                            data = el.Data,
                            description = el.Description,
                            timeStamp = el.TimeStamp
                        });
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetEventLog : {e.ToString()}");
                return null;
            }
        }

        public static List<EventLog.Info> GetEventLogRange(DateTime fromTime, DateTime toTime)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var els = (from el in ctx.EventLog where (el.TimeStamp >= fromTime && el.TimeStamp <= toTime) orderby el.Idx descending select el).ToList();
                    List<EventLog.Info> result = new List<EventLog.Info>();
                    foreach (var el in els)
                    {
                        result.Add(new EventLog.Info()
                        {
                            idx = el.Idx,
                            code = el.Code,
                            data = el.Data,
                            description = el.Description,
                            timeStamp = el.TimeStamp
                        });
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetEventLogRange : {e.ToString()}");
                return null;
            }
        }
    }
}