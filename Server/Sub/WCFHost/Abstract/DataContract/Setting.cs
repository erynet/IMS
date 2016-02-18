using System.Runtime.Serialization;

namespace IMS.Server.Sub.WCFHost.Abstract.DataContract
{
    [DataContract]
    public class IMSSetting
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public int Type { get; set; }
    }
}