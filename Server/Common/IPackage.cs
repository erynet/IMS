using System;

namespace IMS.Server.Common
{
    public interface IPackage : IBusMessage
    {
        string MessageType { get; set; }
        string From { get; set; }
        string Message { get; set; }
        DateTime TimeStamp { get; set; }
        object Luggage { get; set; }
    }
}