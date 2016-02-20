using IMS.Server.Sub.WCFHost.Abstract.DataContract;

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

        public Group(IMSGroup other)
        {
            ID = uid++;

            ParseServerData(other);
        }

        public void ParseServerData(IMSGroup other)
        {
            Data = new Info {
                isUsing = other.Enabled,
                groupID = other.Idx ?? -1,
                groupNo = other.No,
                isGroupVisible = other.Display,
                groupName = other.Name,
                coordinate = new Point(other.CoordX, other.CoordY),
                upsList = new IntList()
            };

            foreach (var otherUps in other.UpsList) {
                if (otherUps.Idx != null) {
                    Data.upsList.Add(otherUps.Idx.Value);
                }
            }
        }

        public IMSGroup ServerData()
        {
            var ret = new IMSGroup {
                Enabled = Data.isUsing,
                Idx = ID,
                No = Data.groupNo,
                Display = Data.isGroupVisible,
                Name = Data.groupName,
                CoordX = Data.coordinate.X,
                CoordY = Data.coordinate.Y,
                // UpsList = Data.upsList.ToString()  
            };

            return ret;
        }
    }
}