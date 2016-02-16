using System.Collections.Generic;

namespace IMS.Client.Core {
    public class Group {
        public class Info {
            private List<int> upsList = new List<int>();

            public bool isUsing { get; set; }
            public int groupNumber { get; set; }
            public bool isGroupVisible { get; set; }
            public string groupName { get; set; }
            public bool isSeperatelyUsing { get; set; }
            public Point coordinate { get; set; }

            public List<int> UpsList => upsList;
        }

        static private int uid = 0;

        public Info Data = new Info();
        public int ID { get; private set; }

        public Group()
        {
            ID = uid++;
        }
    }
}
