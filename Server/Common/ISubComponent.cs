using System;

namespace IMS.Server.Common
{
    public interface ISubComponent : IDisposable
    {
        bool Initialize(int initialCheckIntervalMs);
        bool ConnectBus(BusHub busHub);
        new void Dispose();
        event EventHandler Log;
        // 해당 로그를 발송하기 위한 편의 함수
        void L(string message, LogEvt.MessageType type);
    }
}