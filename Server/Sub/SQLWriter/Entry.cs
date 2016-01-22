using System;
using System.Diagnostics;
using System.Threading;

using IMS.Server.Common;

namespace IMS.Server.Sub.SQLWriter
{
    public class Entry : ISubComponent
    {
        public string NameSpace = "IMS.Server.Sub.SQLWriter.Entry";
        public event EventHandler Log;
        private BusHub _busHub;
        private int _initialCheckIntervalMs;
        private readonly Stopwatch _stw;
        private bool _loopContinue;

        public Entry()
        {
            Log = null;
            _busHub = null;
        }

        public bool ConnectBus(BusHub busHub)
        {
            _busHub = busHub;
            //_packageFlowQueue = new BlockingCollection<IPackage>();
            //busHub.Subscribe<IPackage>((m) => { Router(m); });
            busHub.Subscribe<IPackage>(Router);
            return true;
        }

        private void Router(IPackage package)
        {
            // MessageType 등을 보고 어떤 동작을 취할지 결정하는 함수.
        }

        public bool Initialize(int initialCheckIntervalMs)
        {
            if (Log == null) return false;

            _initialCheckIntervalMs = initialCheckIntervalMs;
            try
            {
                Thread t = new Thread(Run);
                t.Start();
            }
            catch (ThreadStateException)
            {
                // 로그 넘기는 파트 만들어야 한다.
                // 로그 객체 형태를 어떻게 공유할지 고민좀.
            }
            catch (OutOfMemoryException)
            {

            }
            catch (Exception)
            {

            }
            return true;
        }

        private void Run()
        {
            while (_loopContinue)
            {

            }
        }

        public void L(string message, LogEvt.MessageType type)
        {
            Log?.Invoke(this, new LogEvt(message, type, new StackTrace(), NameSpace));
        }

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        protected virtual void Dispose(bool disposing)
        {
            _loopContinue = false;
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~Entry() {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
