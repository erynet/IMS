using System;

namespace IMS.Client.Core.Data
{
    public class EventLog
    {
        public class Info
        {
            public int idx { get; set; }
            public int code { get; set; }
            public string data { get; set; }
            public string description { get; set; }
            public DateTime timeStamp { get; set; }


            public Info()
            {

            }

            public void Copy(Info el)
            {
                idx = el.idx;
                code = el.code;
                data = el.data;
                description = el.description;
                timeStamp = el.timeStamp;
            }

            public Info Clone()
            {
                var clone = new Info();
                clone.Copy(this);

                return clone;
            }
        }

        static private int uid = 0;

        public WarningLog.Info Data = new WarningLog.Info();
        public int ID { get; private set; }

        public EventLog()
        {
            ID = uid++;
        }
    }
}