using System;
using System.Collections.Generic;
using System.Linq;
using IMS.Database.LocalDB;

namespace IMS.Client.Core.Data.DB
{
    public partial class LocalDBDriver
    {
        #region CduSocket

        public static List<CduSocket.Info> GetCduSocketsByIdx(int cduIdx)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var socks =
                        (from s in ctx.CduSocket where s.CduIdx == cduIdx orderby s.No ascending select s).ToList();
                    List<CduSocket.Info> result = new List<CduSocket.Info>();
                    foreach (var s in socks)
                    {
                        result.Add(new CduSocket.Info()
                        {
                            isUsing = s.Enabled,
                            cduIdx = s.CduIdx,
                            cduSocketIdx = s.Idx,
                            cduSocketNo = s.No,
                            cduSocketName = s.Name
                        });
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"GetCduSockets : {e.ToString()}");
                return new List<CduSocket.Info>();
            }
        }

        #endregion

     }
}