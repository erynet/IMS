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
        private GroupManagePage groupManage = new GroupManagePage();
        private UPSManagePage upsManage = new UPSManagePage();
        private PanelManagePage panelManage = new PanelManagePage();
        private AddEquipmentPage addEquipment = new AddEquipmentPage();

        public MainWindow parent;

        public EquipPage()
        {
            InitializeComponent();

            panelManage.parent = this;
            upsManage.parent = this;
            groupManage.parent = this;

            MainFrame.Navigate(groupManage);
        }

        private void button_ups_setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(upsManage);
            button_group_setting.Background = Brushes.Transparent;
            button_ups_setting.Background = Brushes.LightBlue;
            button_panel_setting.Background = Brushes.Transparent;
        }

        private void button_group_setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(groupManage);
            button_group_setting.Background = Brushes.LightBlue;
            button_ups_setting.Background = Brushes.Transparent;
            button_panel_setting.Background = Brushes.Transparent;
        }

        private void button_panel_setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(panelManage);
            button_group_setting.Background = Brushes.Transparent;
            button_ups_setting.Background = Brushes.Transparent;
            button_panel_setting.Background = Brushes.LightBlue;
        }

        private void button_add_equipment_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(addEquipment);
        }

        public void DotManagePopup(int panelID)
        {
            var popup = parent.CreatePopup<DotManagePopup>();
            popup.Refresh(panelID);
            popup.Show();
        }

        public void UpsInfoPopup(int upsNo)
        {
            var popup = parent.CreatePopup<UPSInfoPopup>();
            popup.Refresh(upsNo);
            popup.Show();
        }

        public void UpsRefresh()
        {
            upsManage.Refresh();
            panelManage.Refresh();
            groupManage.Refresh();
            parent.MapRefresh();
        }

        public void UpsRefreshExceptUps()
        {
            panelManage.Refresh();
            groupManage.Refresh();
            parent.MapRefresh();
        }

        public void PanelRefresh()
        {
            upsManage.Refresh();
            panelManage.Refresh();
        }

        public void PanelRefreshExceptPanel()
        {
            upsManage.Refresh();
        }

        public void GroupRefresh()
        {
            upsManage.Refresh();
            panelManage.Refresh();
            groupManage.Refresh();
            parent.MapRefresh();
        }

        public void GroupRefreshExceptGroup()
        {
            upsManage.Refresh();
            panelManage.Refresh();
            parent.MapRefresh();
        }
    }
}
