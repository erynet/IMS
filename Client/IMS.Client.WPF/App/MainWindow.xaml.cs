using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using IMS.Client.Core;
using IMS.Client.Core.Data;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private EquipPage equip;
        private AlarmPage alarm;
        private DataPage data;
        private MapPage map;
        private SettingPage setting;
        private SpecialEventPage special;

        private List<Window> popupList = new List<Window>();

        public MainWindow()
        {
            InitializeComponent();

            DataManager.inst.Init();

            // Child pages
            equip = new EquipPage();
            alarm = new AlarmPage();
            data = new DataPage();
            map = new MapPage();
            setting = new SettingPage();
            special = new SpecialEventPage();

            Update(null, null);

            var timer = new DispatcherTimer();
            timer.Tick += Update;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            Closing += OnClose;

            setting.parent = this;
            equip.parent = this;
            map.parent = this;

            MainFrame.Navigate(map);
        }

        private void Update(object sender, EventArgs e)
        {
            Title = "ETI IMS - Version 0.1 - " + String.Format(@"{0:yyyy년 MM월 dd일 - HH시 mm분 ss초}", DateTime.Now);
        }

        private void OnClose(object sender, EventArgs e)
        {
            foreach (var popup in popupList) {
                popup.Close();
            }
        }

        public T CreatePopup<T>() where T : Window, new()
        {
            var popup = new T();
            popupList.Add(popup);

            return popup;
        }

        public void NavigateToSpecialEvent()
        {
            MainFrame.Navigate(special);
        }

        public void MapRefresh()
        {
            map.Refresh();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("정말 종료하시겠습니까?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) {
                e.Cancel = true;
            }
            else {
                base.OnClosing(e);
            }
        }

        // Main menu
        private void button_map_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(map);
        }

        private void button_list_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(equip);
        }

        private void button_alarm_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(alarm);
        }

        private void button_data_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(data);
        }

        private void button_setting_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(setting);
        }

        private void button_special_event_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(special);
        }

        private void button_help_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
