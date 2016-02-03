using System;
using System.Windows;
using System.Windows.Threading;

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

            Update(null, null);

            var timer = new DispatcherTimer();
            timer.Tick += Update;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            MainFrame.Navigate(map);
        }

        private void Update(object sender, EventArgs e)
        {
            Title = "ETI IMS - version 0.1 - " + String.Format(@"{0:yyyy년 MM월 dd일 - HH시 mm분 ss초}", DateTime.Now);
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
