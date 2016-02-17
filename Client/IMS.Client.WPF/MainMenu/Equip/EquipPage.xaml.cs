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
        private GroupManagePage groupManage = new GroupManagePage();
        private PanelManagePage panelManage = new PanelManagePage();
        private AddEquipmentPage addEquipment = new AddEquipmentPage();

        public MainWindow parent;

        public EquipPage()
        {
            InitializeComponent();

            panelManage.parent = this;
            upsManage.parent = this;
            groupManage.parent = this;

            MainFrame.Navigate(upsManage);
        }

        private void button_ups_setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(upsManage);
        }

        private void button_group_setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(groupManage);
        }

        private void button_panel_setting_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(panelManage);
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

        public void UpsInfoPopup(int upsID)
        {
            var popup = parent.CreatePopup<UPSInfoPopup>();
            popup.Refresh(upsID);
            popup.Show();
        }

        public void GroupPopupCreate()
        {
            var popup = parent.CreatePopup<GroupManipulatePopup>();
            popup.CreateMode(this);
            popup.Show();
        }

        public void GroupPopupEdit(int groupID)
        {
            var popup = parent.CreatePopup<GroupManipulatePopup>();
            popup.EditMode(this, groupID);
            popup.Show();
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
