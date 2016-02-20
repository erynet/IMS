namespace IMS.Client.Core.Data {
    public class Group {
        public class Info {
            public bool isUsing { get; set; }
            public int groupIdx { get; set; }
            public int groupNo { get; set; }
            public bool isGroupVisible { get; set; }
            public string groupName { get; set; }
            public Point coordinate { get; set; }
            public IntList upsIdxList { get; set; }
            public IntList upsNoList { get; set; }

            public Info()
            {
                coordinate = new Point();
                upsIdxList = new IntList();
                upsNoList = new IntList();
            }

            public void Copy(Info rhs)
            {
                isUsing = rhs.isUsing;
                groupIdx = rhs.groupIdx;
                groupNo = rhs.groupNo;
                isGroupVisible = rhs.isGroupVisible;
                groupName = rhs.groupName;
                coordinate = new Point(rhs.coordinate);
                upsIdxList = new IntList(rhs.upsIdxList);
                upsNoList = new IntList(rhs.upsNoList);
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