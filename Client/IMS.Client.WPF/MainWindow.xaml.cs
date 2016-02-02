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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        EquipPage equip = new EquipPage();
        AlarmPage alarm = new AlarmPage();
        DataPage data = new DataPage();
        MapPage map = new MapPage();
        SettingPage setting = new SettingPage();

        public MainWindow()
        {
            InitializeComponent();

            var popup = new UPSInfoPopup();
            popup.Show();
        }

        // Main menu
        private void button_equip_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(equip);
        }

        private void button_alarm_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(alarm);
        }

        private void button_data_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(data);
        }

        private void button_map_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(map);
        }

        private void button_setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(setting);
        }
    }
}
