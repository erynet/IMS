using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Client.Core {
    public class Panel {
        public class Info {
            public bool isUsing { get; set; }
            public int panelID { get; set; }
            public string panelName { get; set; }
            public bool isExtended { get; set; }
            public IntList upsList { get; set; }
            public string installDate { get; set; }
            public string ip { get; set; }

            public Info()
            {
                upsList = new IntList();
            }

            public void Copy(Info rhs)
            {
                isUsing = rhs.isUsing;
                panelID = rhs.panelID;
                panelName = rhs.panelName;
                isExtended = rhs.isExtended;
                upsList = new IntList(rhs.upsList);
                installDate = rhs.installDate;
                ip = rhs.ip;
            }
        }

        public Info Data = new Info();

        static private int uid = 0;
        public int ID { get; private set; }

        public Panel()
        {
            ID = uid++;
        }

        public Panel(IMSCdu other)
        {
            ID = uid++;

            Data = new Info {
                isUsing = other.Enabled,
                panelID = other.Idx ?? -1,
                panelName = other.Name,
                isExtended = other.Extendable,
                upsList = new IntList(),
                installDate = other.InstallAt,
                ip = other.IpAddress
            };

            Data.upsList = IntList.Parse(other.UpsList);
        }

        public IMSCdu ServerData()
        {
            var ret = new IMSCdu {
                Enabled = Data.isUsing,
                Idx = ID,
                Name = Data.panelName,
                Extendable = Data.isExtended,
                UpsList = Data.upsList.ToString(),
                InstallAt = Data.installDate,
                IpAddress = Data.ip
            };

            return ret;
        }
    }
}