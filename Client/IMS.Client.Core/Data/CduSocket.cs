namespace IMS.Client.Core.Data
{
    public class CduSocket
    {
        public class Info
        {
            public bool isUsing { get; set; }
            public int cduIdx { get; set; }
            //public int cduNo { get; set; }
            public int cduSocketIdx { get; set; }
            public int cduSocketNo { get; set; }
            public string cduSocketName { get; set; }
            

            public Info()
            {
                
            }

            public void Copy(Info rhs)
            {
                isUsing = rhs.isUsing;
                cduIdx = rhs.cduIdx;
                cduSocketIdx = rhs.cduSocketIdx;
                cduSocketNo = rhs.cduSocketNo;
                cduSocketName = rhs.cduSocketName;
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

        public CduSocket()
        {
            ID = uid++;
        }
    }
}