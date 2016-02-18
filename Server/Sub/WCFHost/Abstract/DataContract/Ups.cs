using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IMS.Server.Sub.WCFHost.Abstract.DataContract
{
    [DataContract]
    public class IMSUps
    {
        [DataMember]
        public int? Idx { get; set; }
        [DataMember]
        public int GroupIdx { get; set; }
        [DataMember]
        public int No { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<IMSUps> MateList { get; set; }
        [DataMember]
        public int? CduNo { get; set; }
        [DataMember]
        public string Specification { get; set; }
        [DataMember]
        public string Capacity { get; set; }
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
    public class IMSUpsStatus
    {
        
    }

    [DataContract]
    public class IMSUpsEvent
    {
        [DataMember]
        public int Idx { get; set; }
        [DataMember]
        public int UpsIdx { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Body { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public DateTime TimeStamp { get; set; }
    }
}