using System;
using System.Collections.Generic;
using System.Linq;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public partial class LocalDBDriver
    {
        public static List<WarningLog.Info> GetWarningLog(DateTime fromTime)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var wls = (from wl in ctx.WarningLog where wl.TimeStamp >= fromTime orderby wl.Idx descending select wl).ToList();
                    List<WarningLog.Info> result = new List<WarningLog.Info>();
                    foreach (var wl in wls)
                    {
                        result.Add(new WarningLog.Info()
                        {
                            idx = wl.Idx,
                            code = wl.Code,
                            data = wl.Data,
                            deviceNo = wl.DeviceNo,
                            priority = wl.Priority,
                            description = wl.Description,
                            timeStamp = wl.TimeStamp
                        });
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetWaringLog : {e.ToString()}");
                return null;
            }
        }

        public static List<WarningLog.Info> GetWarningLogRange(DateTime fromTime, DateTime toTime)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var wls = (from wl in ctx.WarningLog where (wl.TimeStamp >= fromTime && wl.TimeStamp <= fromTime) orderby wl.Idx descending select wl).ToList();
                    List<WarningLog.Info> result = new List<WarningLog.Info>();
                    foreach (var wl in wls)
                    {
                        result.Add(new WarningLog.Info()
                        {
                            idx = wl.Idx,
                            code = wl.Code,
                            data = wl.Data,
                            deviceNo = wl.DeviceNo,
                            priority = wl.Priority,
                            description = wl.Description,
                            timeStamp = wl.TimeStamp
                        });
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetWaringLogRange : {e.ToString()}");
                return null;
            }
        }
    }
}