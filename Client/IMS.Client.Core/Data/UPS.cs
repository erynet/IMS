using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Client.Core {
    public class Ups {
        public class Info {
            public bool isUsing { get; set; }
            public int upsID { get; set; }
            public int groupID { get; set; }
            public string upsName { get; set; }
            public IntList partnerList { get; set; }
            public int panelID { get; set; }
            public string batteryDescription { get; set; }
            public string batteryCapacity { get; set; }
            public string ip { get; set; }
            public string installDate { get; set; }

            public Info()
            {
                partnerList = new IntList();
            }

            public void Copy(Info rhs)
            {
                isUsing = rhs.isUsing;
                upsID = rhs.upsID;
                groupID = rhs.groupID;
                upsName = rhs.upsName;
                partnerList = new IntList(rhs.partnerList);
                panelID = rhs.panelID;
                batteryDescription = rhs.batteryDescription;
                batteryCapacity = rhs.batteryCapacity;
                ip = rhs.ip;
                installDate = rhs.installDate;
            }
        }

        static private int uid = 0;

        public Info Data = new Info();
        public int ID { get; private set; }

        public Ups()
        {
            ID = uid++;
        }

        public Ups(IMSUps other)
        {
            ID = uid++;

            Data = new Info {
                isUsing = other.Enabled,
                upsID = other.Idx ?? -1,
                groupID = other.GroupIdx,
                upsName = other.Name,
                partnerList = new IntList(),
                panelID = other.CduNo ?? -1,
                batteryDescription = other.Description,
                batteryCapacity = other.Capacity,
                ip = other.IpAddress,
                installDate = other.InstallAt
            };

            Data.partnerList = IntList.Parse(other.MateList);
        }

        public IMSUps ServerData()
        {
            var ret = new IMSUps {
                Enabled = Data.isUsing,
                Idx = ID,
                GroupIdx = Data.groupID,
                Name = Data.upsName,
                MateList = Data.partnerList.ToString(),
                Description = Data.batteryDescription,
                Capacity = Data.batteryCapacity,
                IpAddress = Data.ip,
                InstallAt = Data.installDate
            };

            return ret;
        }
    }
}