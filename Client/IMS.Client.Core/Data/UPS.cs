namespace IMS.Client.Core.Data {
    public class Ups {
        public class Info {
            public bool isUsing { get; set; }
            public int upsID { get; set; }
            public int upsNo { get; set; }
            public int groupID { get; set; }
            public string upsName { get; set; }
            public IntList partnerList { get; set; }
            public int cduID { get; set; }
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
                upsNo = rhs.upsNo;
                groupID = rhs.groupID;
                upsName = rhs.upsName;
                partnerList = new IntList(rhs.partnerList);
                cduID = rhs.cduID;
                batteryDescription = rhs.batteryDescription;
                batteryCapacity = rhs.batteryCapacity;
                ip = rhs.ip;
                installDate = rhs.installDate;
            }

            public Info Clone()
            {
                var clone = new Info();
                clone.Copy(this);

                return clone;
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