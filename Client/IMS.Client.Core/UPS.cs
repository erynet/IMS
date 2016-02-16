namespace IMS.Client.Core {
    public class Ups {
        public class Info {
            public bool isUsing { get; set; }
            public int groupNumber { get; set; }
            public bool isGroupVisible { get; set; }
            public string groupName { get; set; }
            public bool isSeparatelyUseable { get; set; }
            public int upsID { get; set; }
            public string partnerIDs { get; set; }
            public string upsName { get; set; }
            public string batteryCapacity { get; set; }
            public string batteryDescription { get; set; }
            public string ip { get; set; }
            public string installDate { get; set; }
            public string coordinate { get; set; }
        }

        public Info Data = new Info();
    }
}
