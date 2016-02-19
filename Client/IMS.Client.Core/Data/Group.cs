﻿using IMS.Server.Sub.WCFHost.Abstract.DataContract;

namespace IMS.Client.Core {
    public class Group {
        public class Info {
            public bool isUsing { get; set; }
            public int groupID { get; set; }
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
                isGroupVisible = rhs.isGroupVisible;
                groupName = rhs.groupName;
                coordinate = new Point(rhs.coordinate);
                upsList = new IntList(rhs.upsList);
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

            Data = new Info {
                isUsing = other.Status == null ? false : other.Status.Value == 1,
                groupID = other.Idx ?? -1,
                isGroupVisible = other.Enabled,
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
    }
}