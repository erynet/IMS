using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IMS.Server.Sub.WCFHost.Abstract.DataContract
{
    [DataContract]
    public class IMSCdu
    {
        [DataMember]
        public int? Idx { get; set; }
        [DataMember]
        public int? GroupIdx { get; set; }
        [DataMember]
        public int No { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string UpsList { get; set; }
        [DataMember]
        public bool Extendable { get; set; }
        [DataMember]
        public int? ContractCount { get; set; }
        [DataMember]
        public string IpAddress { get; set; }
        [DataMember]
        public int? Status { get; set; }
        [DataMember]
        public bool Enabled { get; set; }
        [DataMember]
        public string InstallAt { get; set; }
        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class IMSCduStatus
    {
        
    }

    [DataContract]
    public class IMSCduEvent
    {
        [DataMember]
        public int Idx { get; set; }
        [DataMember]
        public int CduIdx { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public DateTime TimeStamp { get; set; }
    }

    [DataContract]
    public class IMSCduSocket
    {
        [DataMember]
        public int? Idx { get; set; }
        [DataMember]
        public int CduIdx { get; set; }
        [DataMember]
        public int No { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool Enabled { get; set; }
    }

}