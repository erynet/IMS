using System;
using System.Diagnostics;
using System.Security.Policy;
using System.ServiceModel;
using System.ServiceModel.Description;
using IMS.Server.Sub.WCFHost.Abstract;

namespace IMS.Server.Sub.WCFHost
{
    public class Host
    {
        public readonly string NameSpace = "IMS.Server.Sub.WCFHost.Host";

        private ServiceHost _serviceHost;

        public Host()
        {
            /*
            try
            {
                _serviceHost = new ServiceHost(typeof(IIMS));

                _serviceHost.Opening += ReportServiceState;
                _serviceHost.Opened += ReportServiceState;
                _serviceHost.Closing += ReportServiceState;
                _serviceHost.Closed += ReportServiceState;
                _serviceHost.Faulted += ReportServiceState;
            }
            */
            
            
        }


    }
}