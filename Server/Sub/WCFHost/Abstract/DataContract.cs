using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IMS.Server.Sub.WCFHost.Abstract
{
    [DataContract]
    public class Event
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
    public class Warning
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
    public class Setting
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public int Type { get; set; }
    }

    [DataContract]
    public class DeviceStatus
    {
        [DataMember]
        public int Idx { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public object Value { get; set; }
    }
}