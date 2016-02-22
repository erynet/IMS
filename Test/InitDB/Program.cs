using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using IMS.Database.LocalDB;
using IMS.Database.LocalDB.Model;

namespace IMS.Test.InitDB
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new LocalDB())
            {
                ctx.Database.Initialize(true);

                Group g1 = new Group()
                {
                    No = 1,
                    Name = "그룹01",
                    Display = true,
                    CoordX = 400,
                    CoordY = 600,
                    Status = 0,
                    Enabled = true,
                    Description = null
                };

                Group g2 = new Group()
                {
                    No = 2,
                    Name = "그룹02",
                    Display = true,
                    CoordX = 600,
                    CoordY = 800,
                    Status = 0,
                    Enabled = true,
                    Description = null
                };

                using (var trx = new TransactionScope())
                {
                    ctx.Group.Add(g1);
                    ctx.Group.Add(g2);
                    ctx.SaveChanges();

                    trx.Complete();
                }

                CDU c1 = new CDU()
                {
                    GroupNo = 1,
                    No = 1,
                    Name = "CDU01",
                    Extendable = true,
                    IpAddress = "192.168.1.2",
                    Status = 0,
                    Enabled = true,
                    InstallAt = "2015.1.14",
                    Description = null
                };

                CDU c2 = new CDU()
                {
                    GroupNo = 1,
                    No = 2,
                    Name = "CDU02",
                    Extendable = true,
                    IpAddress = "192.168.1.3",
                    Status = 0,
                    Enabled = true,
                    InstallAt = "2015.1.14",
                    Description = null
                };

                CDU c3 = new CDU()
                {
                    GroupNo = 1,
                    No = 3,
                    Name = "CDU03",
                    Extendable = true,
                    IpAddress = "192.168.1.4",
                    Status = 0,
                    Enabled = true,
                    InstallAt = "2015.1.14",
                    Description = null
                };

                using (var trx = new TransactionScope())
                {
                    ctx.Cdu.Add(c1);
                    ctx.Cdu.Add(c2);
                    ctx.Cdu.Add(c3);
                    ctx.SaveChanges();

                    trx.Complete();
                }

                UPS u1 = new UPS()
                {
                    GroupNo = 1,
                    No = 1,
                    Name = "UPS01",
                    MateList = null,
                    CduNo = 1,
                    Specification = "듀라셀",
                    Capacity = "2Kw",
                    IpAddress = "192.168.0.2",
                    Status = 0,
                    Enabled = true,
                    InstallAt = "2016.1.15",
                    Description = null
                };

                UPS u2 = new UPS()
                {
                    GroupNo = 1,
                    No = 2,
                    Name = "UPS02",
                    MateList = null,
                    CduNo = 1,
                    Specification = "듀라셀",
                    Capacity = "2Kw",
                    IpAddress = "192.168.0.3",
                    Status = 0,
                    Enabled = true,
                    InstallAt = "2016.1.15",
                    Description = null
                };
                
                UPS u3 = new UPS()
                {
                    GroupNo = 1,
                    No = 3,
                    Name = "UPS03",
                    MateList = null,
                    CduNo = 2,
                    Specification = "듀라셀",
                    Capacity = "2Kw",
                    IpAddress = "192.168.0.4",
                    Status = 0,
                    Enabled = true,
                    InstallAt = "2016.1.15",
                    Description = null
                };
                
                UPS u4 = new UPS()
                {
                    GroupNo = 2,
                    No = 4,
                    Name = "UPS04",
                    MateList = null,
                    CduNo = 3,
                    Specification = "로케트",
                    Capacity = "2Kw",
                    IpAddress = "192.168.0.5",
                    Status = 0,
                    Enabled = true,
                    InstallAt = "2016.1.15",
                    Description = null
                };

                UPS u5 = new UPS()
                {
                    GroupNo = 2,
                    No = 5,
                    Name = "UPS05",
                    MateList = null,
                    CduNo = 3,
                    Specification = "로케트",
                    Capacity = "2Kw",
                    IpAddress = "192.168.0.6",
                    Status = 0,
                    Enabled = true,
                    InstallAt = "2016.1.15",
                    Description = null
                };

                using (var trx = new TransactionScope())
                {
                    ctx.Ups.Add(u1);
                    ctx.Ups.Add(u2);
                    ctx.Ups.Add(u3);
                    ctx.Ups.Add(u4);
                    ctx.Ups.Add(u5);
                    ctx.SaveChanges();

                    trx.Complete();
                }

                
            }
        }
    }
}
