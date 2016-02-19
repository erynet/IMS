using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace IMS.Client.Core {
    public class NetworkManager {
        private IMSClient client;
        private Thread updateThread;

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
                }
            }
            catch (Exception ex) {

            }
        }

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
    }
}
