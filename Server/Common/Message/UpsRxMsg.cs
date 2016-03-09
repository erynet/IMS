using System;
using IMS.Server.Common.PacketStruct;

namespace IMS.Server.Common.Message
{
    public class UpsRxMsg : IPackage
    {
        public string From { get; set; }
        public object Luggage { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public object Sender { get; set; }
        public DateTime TimeStamp { get; set; }

        public UpsRxMsg(string message, string from, UpsTotalStruct uts)
        {
            Message = message;
            From = from;
            Luggage = (object) uts;
            MessageType = 101;
            Sender = null;
            TimeStamp = DateTime.Now;
        }
    }
}