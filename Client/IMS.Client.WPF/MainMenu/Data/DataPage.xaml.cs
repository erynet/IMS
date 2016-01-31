using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for DataPage.xaml
    /// </summary>
    public partial class DataPage : Page {
        private UPSViewPage upsView = new UPSViewPage();
        private AnalogViewPage analogView = new AnalogViewPage();
        private LogViewPage logView = new LogViewPage();
        private ErrorUPSViewPage errorUPSView = new ErrorUPSViewPage();

        public DataPage()
        {
            InitializeComponent();

            upsView.SetParent(this);
            analogView.SetParent(this);
            logView.SetParent(this);
            errorUPSView.SetParent(this);

            NavigateToUPS();
        }

        public void NavigateToUPS()
        {
            MainFrame.Navigate(upsView);
        }

        public void NavigateToAnalog()
        {
            MainFrame.Navigate(analogView);
        }

        public void NavigateToLog()
        {
            MainFrame.Navigate(logView);
        }

        public void NavigateToErrorUPS()
        {
            MainFrame.Navigate(errorUPSView);
        }
    }
}
