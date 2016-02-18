using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IMS.Server.Sub.WCFHost.Abstract.DataContract
{
    [DataContract]
    public class IMSGroup
    {
        [DataMember]
        public int? Idx { get; set; }
        [DataMember]
        public int No { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool Display { get; set; }
        [DataMember]
        public int CoordX { get; set; }
        [DataMember]
        public int CoordY { get; set; }
        [DataMember]
        public List<IMSUps> UpsList { get; set; }
        [DataMember]
        public List<IMSCdu> CduList { get; set; }
        [DataMember]
        public int? Status { get; set; }
        [DataMember]
        public bool Enabled { get; set; }
        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class IMSGroups
    {
        [DataMember]
        public List<IMSGroup> Groups { get; set; }

        [DataMember]
        public DateTime At { get; set; }
    }
}