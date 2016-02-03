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
    /// Interaction logic for UPSViewPage.xaml
    /// </summary>
    public partial class DigitalViewPage : Page {
        private UPSInfoPopup parent;

        public DigitalViewPage()
        {
            InitializeComponent();
        }

        public void SetParent(UPSInfoPopup parent)
        {
            this.parent = parent;
        }

        private void button_analog_Click(object sender, RoutedEventArgs e)
        {
            parent.NavigateToAnalog();
        }

        private void button_see_log_Click(object sender, RoutedEventArgs e)
        {
            parent.NavigateToLog();
        }

        private void button_error_list_Click(object sender, RoutedEventArgs e)
        {
            parent.NavigateToErrorUPS();
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            parent.Close();
        }
    }
}
