using System.Collections.Generic;

namespace IMS.Client.Core {
    public class Group {
        public class Info {
            public bool isUsing { get; set; }
            public int groupNumber { get; set; }
            public bool isGroupVisible { get; set; }
            public string groupName { get; set; }
            public bool isSeperatelyUsing { get; set; }
            public string coordinate { get; set; }
        }

        public Info Data = new Info();
    }
}
