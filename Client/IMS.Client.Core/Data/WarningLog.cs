using System;

namespace IMS.Client.Core.Data
{
    public class WarningLog
    {
        public class Info
        {
            public int idx { get; set; }
            public int code { get; set; }
            public string data { get; set; }
            public int deviceNo { get; set; }
            public int priority { get; set; }
            public string description { get; set; }
            public DateTime timeStamp { get; set; }


            public Info()
            {

            }

            public void Copy(Info wl)
            {
                idx = wl.idx;
                code = wl.code;
                data = wl.data;
                deviceNo = wl.deviceNo;
                priority = wl.priority;
                description = wl.description;
                timeStamp = wl.timeStamp;
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

        public WarningLog()
        {
            ID = uid++;
        }
    }
}