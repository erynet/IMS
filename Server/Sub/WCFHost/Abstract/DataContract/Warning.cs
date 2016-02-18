using System.Runtime.Serialization;

namespace IMS.Server.Sub.WCFHost.Abstract.DataContract
{
    [DataContract]
    public class IMSWarning
    {
        [DataMember]
        public int Idx { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public object Data { get; set; }
    }
}