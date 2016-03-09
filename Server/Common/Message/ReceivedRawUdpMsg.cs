using System;
using IMS.Server.Common.PacketStruct;

namespace IMS.Server.Common.Message
{
    public class ReceivedRawUdpMsg : IPackage
    {
        public string From { get; set; }
        public object Luggage { get; set; }
        public string Message { get; set; }
        public int MessageType { get; set; }
        public object Sender { get; set; }
        public DateTime TimeStamp { get; set; }
        public int ReceivedSize { get; set; }

        public ReceivedRawUdpMsg(string from, object rawData, int receivedSize)
        {
            Message = null;
            From = from;
            Luggage = (object)rawData;
            MessageType = 0;
            Sender = null;
            TimeStamp = DateTime.Now;
            ReceivedSize = receivedSize;
        }
    }
}