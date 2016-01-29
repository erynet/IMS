using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IMS.Server.Sub.WCFHost.Abstract
{
    [DataContract]
    public class IMSEvent
    {
        [DataMember]
        public int Idx { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public string Data { get; set; }
    }

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

    [DataContract]
    public class IMSDeviceStatus
    {
        [DataMember]
        public int Idx { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public object Value { get; set; }
    }
}