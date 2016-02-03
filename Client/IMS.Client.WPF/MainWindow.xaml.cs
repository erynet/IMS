﻿using System;
using System.Collections.Generic;
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

        private List<Window> popupList = new List<Window>();

        public MainWindow()
        {
            InitializeComponent();

            var popup = CreatePopup<UPSInfoPopup>();
            popup.Show();

            Update(null, null);

            var timer = new DispatcherTimer();
            timer.Tick += Update;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            MainFrame.Navigate(map);

            Closing += OnClose;
        }

        private void Update(object sender, EventArgs e)
        {
            Title = "ETI IMS - version 0.1 - " + String.Format(@"{0:yyyy년 MM월 dd일 - HH시 mm분 ss초}", DateTime.Now);
        }

        private void OnClose(object sender, EventArgs e)
        {
            foreach (var popup in popupList) {
                popup.Close();
            }
        }

        public Window CreatePopup<T>() where T : Window, new()
        {
            var popup = new T();
            popupList.Add(popup);

            return popup;
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
