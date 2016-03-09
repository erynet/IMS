using System;

namespace IMS.Server.Common.Message
{
    public interface IPackage : IBusMessage
    {
        int MessageType { get; set; }
        string From { get; set; }
        string Message { get; set; }
        DateTime TimeStamp { get; set; }
        object Luggage { get; set; }
    }
}