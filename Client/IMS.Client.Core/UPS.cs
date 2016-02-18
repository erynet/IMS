namespace IMS.Client.Core {
    public class Ups {
        public class Info {
            public bool isUsing { get; set; }
            public int upsID { get; set; }
            public int groupID { get; set; }
            public string upsName { get; set; }
            public PartnerList partnerList { get; set; }
            public int panelID { get; set; }
            public string batteryDescription { get; set; }
            public string batteryCapacity { get; set; }
            public string ip { get; set; }
            public string installDate { get; set; }

            public Info()
            {
                partnerList = new PartnerList();
            }

            public void Copy(Info rhs)
            {
                isUsing = rhs.isUsing;
                upsID = rhs.upsID;
                groupID = rhs.groupID;
                upsName = rhs.upsName;
                partnerList = new PartnerList(rhs.partnerList);
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
    }
}
