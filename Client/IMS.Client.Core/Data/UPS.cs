namespace IMS.Client.Core.Data {
    public class Ups {
        public class Info {
            public bool isUsing { get; set; }
            public int upsIdx { get; set; }
            public int upsNo { get; set; }
            public int groupIdx { get; set; }
            public int groupNo { get; set; }
            public string upsName { get; set; }
            public IntList partnerIdxList { get; set; }
            public IntList partnerNoList { get; set; }
            public int cduIdx { get; set; }
            public int cduNo { get; set; }
            public string batteryDescription { get; set; }
            public string batteryCapacity { get; set; }
            public string ip { get; set; }
            public string installDate { get; set; }

            public Info()
            {
                partnerIdxList = new IntList();
                partnerNoList = new IntList();
            }

            public void Copy(Info rhs)
            {
                isUsing = rhs.isUsing;
                upsIdx = rhs.upsIdx;
                upsNo = rhs.upsNo;
                groupIdx = rhs.groupIdx;
                groupNo = rhs.groupNo;
                upsName = rhs.upsName;
                partnerIdxList = new IntList(rhs.partnerIdxList);
                partnerNoList = new IntList(rhs.partnerNoList);
                cduIdx = rhs.cduIdx;
                cduNo = rhs.cduNo;
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