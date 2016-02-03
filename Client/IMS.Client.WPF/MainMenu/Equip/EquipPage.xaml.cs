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
    /// Interaction logic for ListPage.xaml
    /// </summary>
    public partial class EquipPage : Page {
        private UPSManagePage upsManage = new UPSManagePage();
        private PanelManagePage panelManage = new PanelManagePage();

        public EquipPage()
        {
            InitializeComponent();
        }

        private void button_ups_setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(upsManage);
        }

        private void button_add_instrument_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_panel_setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(panelManage);
        }
    }
}
