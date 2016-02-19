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
                isUsing = other.Status == null ? false : other.Status.Value == 1,
                panelID = other.Idx ?? -1,
                panelName = other.Name,
                isExtended = other.Extendable,
                upsList = new IntList(),
                installDate = other.InstallAt,
                ip = other.IpAddress
            };

            foreach (var otherUps in other.UpsList) {
                if (otherUps.Idx != null) {
                    Data.upsList.Add(otherUps.Idx.Value);
                }
            }
        }
    }
}