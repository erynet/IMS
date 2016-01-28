﻿using System;
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
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page {
        private AddEquipmentPage addEquipment = new AddEquipmentPage();

        public SettingPage()
        {
            InitializeComponent();
        }

        private void button_error_popup_save_time_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_data_save_time_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_add_equipment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(addEquipment);
        }
    }
}