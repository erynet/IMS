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
    /// Interaction logic for LogViewPage.xaml
    /// </summary>
    public partial class LogViewPage : Page {
        private UPSInfoPopup parent;

        public LogViewPage()
        {
            InitializeComponent();
        }

        public void SetParent(UPSInfoPopup parent)
        {
            this.parent = parent;
        }

        public void SetID(int upsID)
        {
            equipment_number.Text = upsID.ToString();
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            parent.Close();
        }

        private void button_excel_export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_digital_Click(object sender, RoutedEventArgs e)
        {
            parent.NavigateToUPS();
        }
    }
}
