using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace IMS.Client.Core {
    public class NetworkManager {
        private static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            string sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics) {
                if (sMacAddress == string.Empty)// only return MAC Address from first card  
                {
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }

            return sMacAddress;
        }

        private IMSClient client;
        private Thread updateThread;

        private List<Task<Action>> sendList = new List<Task<Action>>();
        private List<Action> callbackList = new List<Action>();

        private string mac;

        public void Init()
        {
            client = new IMSClient();
            mac = GetMACAddress();
        }

        public void Start()
        {
            updateThread = new Thread(Update);
            updateThread.Start();
        }

        public void End()
        {
            updateThread?.Abort();
            var exitSuccess = client.Leave();
        }

        public void UpdateCallback()
        {
            lock (callbackList) {
                foreach (var callback in callbackList) {
                    callback();
                }

                callbackList.Clear();
            }
        }

        private void Update()
        {
            try {
                var ret = client.Athenticate(mac);

                // Loop
                var time = DateTime.Now;

                while (true) {
                    var now = DateTime.Now;
                    var dt = now - time;
                    time = now;

                    foreach (var send in sendList) {
                        if (send.IsCompleted == true) {
                            lock (callbackList) {
                                callbackList.Add(send.Result);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {

            }
        }

        // Send

        public void SendPacket(Func<Action> func)
        {
            var task = new Task<Action>(func);
            sendList.Add(task);

            task.Start();
        }

        public void SetGroup(Group group)
        {
            SendPacket(() => {
                var ret = client.SetGroup(group.ServerData());

                return () => {
                    if (ret == true) {
                    }
                    else {
                        // Something's wrong
                    }
                };
            });
        }
    }
}
