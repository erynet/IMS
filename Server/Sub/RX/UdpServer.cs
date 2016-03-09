
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using IMS.Server.Common;
using IMS.Server.Common.Message;

namespace IMS.Server.Sub.RX
{
    public class UdpServer : IDisposable
    {
        private readonly string _host;
        private readonly int _port;

        private IPAddress _ipAddress;
        private IPEndPoint _ipEndPoint;
        
        private Socket _sock = null;
        private byte[] _buffer = new byte[32 * 1024];
        
        public BusHub BusHub = null;

        private bool _loopContinue;


        public UdpServer(string host, int port, int maxConnection = 512)
        {
            _host = host;
            _port = port;

            _loopContinue = true;
        }

        public bool Connect()
        {
            try
            {
                _sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _ipAddress = IPAddress.Parse(_host);
                _ipEndPoint = new IPEndPoint(_ipAddress, _port);
                _sock.Bind(_ipEndPoint);
                _sock.ReceiveTimeout = 250;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Run()
        {
            EndPoint remoteEp = null;

            while (_loopContinue)
            {
                try
                {
                    int receiveSize = _sock.ReceiveFrom(_buffer, 0, _buffer.Length, SocketFlags.None, ref remoteEp);
                    BusHub.PublishAsync(new ReceivedRawUdpMsg(remoteEp.ToString(), _buffer.Clone(), receiveSize));
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode == SocketError.TimedOut)
                        continue;
                    Connect();
                }
                catch (Exception)
                {
                    
                }
            }
        }
        
        public void Dispose()
        {
            _loopContinue = false;
        }
    }
}