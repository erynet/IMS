using System;

namespace IMS.Client.Core.Data
{
    public class CduEvent
    {
        public class Info
        {
            public int idx { get; set; }
            public int cduNo { get; set; }
            public string title { get; set; }
            public string body { get; set; }
            public int priority { get; set; }
            public DateTime timeStamp { get; set; }


            public Info()
            {

            }

            public void Copy(Info rhs)
            {
                idx = rhs.idx;
                cduNo = rhs.cduNo;
                title = rhs.title;
                body = rhs.body;
                priority = rhs.priority;
                timeStamp = rhs.timeStamp;
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

        public CduEvent()
        {
            ID = uid++;
        }
    }
}