using System.Collections.Generic;

namespace IMS.Client.Core {
    public class Group {
        public class Info {
            public bool isUsing { get; set; }
            public int groupID { get; set; }
            public bool isGroupVisible { get; set; }
            public string groupName { get; set; }
            public bool isSeperatelyUsing { get; set; }
            public Point coordinate { get; set; }
            public IntList upsList { get; set; }

            public Info()
            {
                coordinate = new Point();
            }

            public void Copy(Info rhs)
            {
                isUsing = rhs.isUsing;
                groupID = rhs.groupID;
                isGroupVisible = rhs.isGroupVisible;
                groupName = rhs.groupName;
                isSeperatelyUsing = rhs.isSeperatelyUsing;
                coordinate = new Point(rhs.coordinate);
                upsList = new IntList();
            }
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
