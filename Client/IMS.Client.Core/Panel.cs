namespace IMS.Client.Core {
    public class Panel {
        public class Info {
            public bool isUsing { get; set; }
            public int panelID { get; set; }
            public string panelName { get; set; }
            public bool isExtended { get; set; }
            public string installDate { get; set; }
            public string ip { get; set; }

            public void Copy(Info rhs)
            {
                isUsing = rhs.isUsing;
                panelID = rhs.panelID;
                panelName = rhs.panelName;
                isExtended = rhs.isExtended;
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
    }
}
