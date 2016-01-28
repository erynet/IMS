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
    public partial class UPSViewPage : Page {
        private DataPage parent;

        public UPSViewPage()
        {
            InitializeComponent();
        }

        public void SetParent(DataPage parent)
        {
            this.parent = parent;
        }

        private void button_analog_Click(object sender, RoutedEventArgs e)
        {
            parent.NavigateToAnalog();
        }
    }
}
