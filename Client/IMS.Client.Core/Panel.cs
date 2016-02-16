namespace IMS.Client.Core {
    public class Panel {
        public class Info {
            public bool isUsing { get; set; }
            public int panelID { get; set; }
            public string panelName { get; set; }
            public bool isExtended { get; set; }
            public string ip { get; set; }
            public string installDate { get; set; }
        }

        public Info Data = new Info();
    }
}
