namespace IMS.Client.Core {
    public class Group {
        public class Info {
            public bool isUsing { get; set; }
            public int groupID { get; set; }
            public int groupNo { get; set; }
            public bool isGroupVisible { get; set; }
            public string groupName { get; set; }
            public Point coordinate { get; set; }
            public IntList upsList { get; set; }

            public Info()
            {
                coordinate = new Point();
                upsList = new IntList();
            }

            public void Copy(Info rhs)
            {
                isUsing = rhs.isUsing;
                groupID = rhs.groupID;
                groupNo = rhs.groupNo;
                isGroupVisible = rhs.isGroupVisible;
                groupName = rhs.groupName;
                coordinate = new Point(rhs.coordinate);
                upsList = new IntList(rhs.upsList);
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

        public Group()
        {
            ID = uid++;
        }
    }
}