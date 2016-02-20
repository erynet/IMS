namespace IMS.Client.Core.Data {
    public class Cdu {
        public class Info {
            public bool isUsing { get; set; }
            public int cduID { get; set; }
            public int cduNo { get; set; }
            public string cduName { get; set; }
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
                cduID = rhs.cduID;
                cduNo = rhs.cduNo;
                cduName = rhs.cduName;
                isExtended = rhs.isExtended;
                upsList = new IntList(rhs.upsList);
                installDate = rhs.installDate;
                ip = rhs.ip;
            }

            public Info Clone()
            {
                var clone = new Info();
                clone.Copy(this);

                return clone;
            }
        }

        public Info Data = new Info();

        static private int uid = 0;
        public int ID { get; private set; }

        public Cdu()
        {
            ID = uid++;
        }
    }
}