using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.ServiceProcess;
using System.Security.Principal;
using System.Configuration.Install;
using System.Diagnostics;
using IMS.Server.Common;

namespace IMS.Server.Host
{
    public class StringTable
    {
        private static readonly string Version = "0.1a";
        public static string ServiceName => $"IMS Service v{Version}";

        public static string Description = "IMS Service Process";
    }

    public class Hybrid
    {
        static void Main(string[] args)
        {
            bool useAppointedLibrary = false;

            var options = new CommandLineOptions();
            
            if (args.Length == 0)
            {
            }
            else
            {
                if (CommandLine.Parser.Default.ParseArguments(args, options))
                {
                    if (options.InstallService || options.UninstallService)
                    {
                        if (options.InstallService)
                        {
                            if (!CheckPrivilege())
                            {
                                Console.WriteLine("This action require administrator privilege");
                                Environment.Exit(0);
                            }
                            ServiceControl.InstallService(StringTable.ServiceName, typeof(HostService).Assembly);
                            ServiceControl.StartService(StringTable.ServiceName);
                        }
                        else
                        {
                            if (!CheckPrivilege())
                            {
                                Console.WriteLine("This action require administrator privilege");
                                Environment.Exit(0);
                            }
                            ServiceControl.StopService(StringTable.ServiceName);
                            ServiceControl.UninstallService(StringTable.ServiceName, typeof(HostService).Assembly);
                        }
                        Environment.Exit(0);
                    }

                    useAppointedLibrary = options.LibName != "NoSuchValue";
                }
                else
                {
                    Environment.Exit(0);
                }
            }


            if (Environment.UserInteractive)
            {
                var core = new Core();

                core.Log += LogEventTerminalReceiver;
                if (useAppointedLibrary)
                {
                    if (!core.Attach(options.LibName))
                    {
                        Console.WriteLine("Error occur in initializing sub components");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    if (!core.Attach())
                    {
                        Console.WriteLine("Error occur in initializing sub components");
                        Environment.Exit(0);
                    }
                }
                
                Console.CancelKeyPress += new ConsoleCancelEventHandler(
                    delegate (object sender, ConsoleCancelEventArgs arg)
                    {
                        //Console.WriteLine("Ctrl-C detected");

                        core.Dispose();
                        arg.Cancel = true;
                    }
                );

                var t = new Thread(core.Run);
                t.Start();
            }
            else
            {
                var cs = new HostService();
                ServiceBase.Run(cs);
            }

        }

        static bool CheckPrivilege()
        {
            var identity = WindowsIdentity.GetCurrent();
            if (identity == null) return false;
            var principal = new WindowsPrincipal(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static void LogEventTerminalReceiver(object sender, EventArgs evt)
        {
            string msg;
            var e = (LogEvt)evt;

            switch (e.Category)
            {
                case LogEvt.MessageType.Comment:
                    // 그냥 내가 하고 싶은 말 띄우기
                    msg = $"[{e.When:MM/dd/yy H:mm:ss zzz}] [{"Comment"}] [{e.From}] {e.Message}";
                    break;
                case LogEvt.MessageType.Debug:
                    // 디버그 목적으로 남기는 메시지
                    msg = $"[{e.When:MM/dd/yy H:mm:ss zzz}] [{"Debug  "}] [{e.From}] {e.Message}";
                    break;
                case LogEvt.MessageType.Info:
                    // 정보 출력 목적으로 남기는 메시지
                    msg = $"[{e.When:MM/dd/yy H:mm:ss zzz}] [{"Info   "}] [{e.From}] {e.Message}";
                    break;
                case LogEvt.MessageType.Warning:
                    // Assert 구문에 해당하는 부분을 통과하지 못했을때 남기는 메시지
                    msg = $"[{e.When:MM/dd/yy H:mm:ss zzz}] [{"Warning"}] [{e.From}] {e.Message}";
                    break;
                case LogEvt.MessageType.Error:
                    // Exception 에 catch 되었을때 남기는 메시지
                    msg = $"[{e.When:MM/dd/yy H:mm:ss zzz}] [{"Error  "}] [{e.From}] {e.Message}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Console.WriteLine(msg);
        }
    }

    public class HostService : ServiceBase
    {
        private Core _core;
        private System.ComponentModel.IContainer components = null;

        public HostService()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ServiceName = StringTable.ServiceName;
            
            _core = new Core();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnStart(string[] args)
        {
            if (!_core.Attach()) return;
            _core.Log += LogEventTerminalReceiver;

            var t = new Thread(_core.Run);
            t.Start();

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _core.Dispose();
            base.OnStop();
        }

        void LogEventTerminalReceiver(object sender, EventArgs evt)
        {
            string msg;
            var e = (LogEvt)evt;

            switch (e.Category)
            {
                case LogEvt.MessageType.Comment:
                    // 그냥 내가 하고 싶은 말 띄우기
                    msg = $"[{"Comment"}] [{e.From}] {e.Message}";
                    break;
                case LogEvt.MessageType.Debug:
                    // 디버그 목적으로 남기는 메시지
                    msg = $"[{"  Debug"}] [{e.From}] {e.Message}";
                    break;
                case LogEvt.MessageType.Info:
                    // 정보 출력 목적으로 남기는 메시지
                    msg = $"[{"   Info"}] [{e.From}] {e.Message}";
                    break;
                case LogEvt.MessageType.Warning:
                    // Assert 구문에 해당하는 부분을 통과하지 못했을때 남기는 메시지
                    msg = $"[{"Warning"}] [{e.From}] {e.Message}";
                    break;
                case LogEvt.MessageType.Error:
                    // Exception 에 catch 되었을때 남기는 메시지
                    msg = $"[{"Comment"}] [{e.From}] {e.Message}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            EventLog.WriteEntry(msg);
        }
    }

    [RunInstallerAttribute(true)]
    public class ClientServiceInstaller : Installer
    {
        public ClientServiceInstaller()
        {
            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };
            ServiceInstaller serviceInstaller = new ServiceInstaller
            {
                StartType = ServiceStartMode.Automatic,
                ServiceName = StringTable.ServiceName,
                Description = StringTable.Description
            };
            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}
