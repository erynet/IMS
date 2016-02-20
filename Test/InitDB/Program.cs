using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Database.LocalDB;

namespace IMS.Test.InitDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new LocalDB();
            ctx.Database.Initialize(true);
        }
    }
}
