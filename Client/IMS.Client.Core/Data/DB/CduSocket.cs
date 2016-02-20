using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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

        public static List<CduSocket.Info> GetCduSocketsByNo(int cduNo)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var socks =
                        (from s in ctx.CduSocket where s.CduIdx == cduNo orderby s.No ascending select s).ToList();
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

        public static bool SetCduSocket(int cduIdx, List<CduSocket.Info> newInfo)
        {
            try
            {
                using (var ctx = new LocalDB())
                {
                    var existSockets = (from s in ctx.CduSocket where s.CduIdx == cduIdx select s).ToList();
                    List<Database.LocalDB.Model.CduSocket> newSockets = new List<Database.LocalDB.Model.CduSocket>();
                    var ns = (from s in newInfo select s).ToList();
                    foreach (var s in ns)
                    {
                        newSockets.Add(new Database.LocalDB.Model.CduSocket()
                        {
                            CduIdx = s.cduIdx,
                            No = s.cduSocketNo,
                            Name = s.cduSocketName,
                            Enabled = s.isUsing
                        });
                    }

                    using (var trx = new TransactionScope())
                    {
                        ctx.CduSocket.RemoveRange(existSockets);
                        ctx.CduSocket.AddRange(newSockets);
                        ctx.SaveChanges();
                        trx.Complete();
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"SetCduSocket : {e.ToString()}");
                return false;
            }
        }

        #endregion

    }
}