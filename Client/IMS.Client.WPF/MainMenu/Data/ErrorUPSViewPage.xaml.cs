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
    /// Interaction logic for ErrorUPSViewPage.xaml
    /// </summary>
    public partial class ErrorUPSViewPage : Page {
        private UPSInfoPopup parent;

        public ErrorUPSViewPage()
        {
            InitializeComponent();
        }

        public void SetParent(UPSInfoPopup parent)
        {
            this.parent = parent;
        }

        private void button_digital_Click(object sender, RoutedEventArgs e)
        {
            parent.NavigateToUPS();
        }
    }
}
