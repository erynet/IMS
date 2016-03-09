using System;
using IMS.Server.Common.PacketStruct;

namespace IMS.Server.Common.Message
{
    public class UpsTxMsg : IPackage
    {
        public string From { get; set; }
        public object Luggage { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public object Sender { get; set; }
        public DateTime TimeStamp { get; set; }

        public UpsTxMsg(string message, string from, int operationCode)
        {
            Message = message;
            From = from;
            Luggage = (object)operationCode;
            MessageType = 100;
            Sender = null;
            TimeStamp = DateTime.Now;
        }
    }
}