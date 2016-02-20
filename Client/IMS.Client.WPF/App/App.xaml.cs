using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        [DllImport("Kernel32.dll")]
        public static extern bool AttachConsole(int processId);

        public App()
        {
            Startup += OnStartup;
        }

        protected void OnStartup(object sender, StartupEventArgs e)
        {
            AttachConsole(-1);
        }
    }
}
