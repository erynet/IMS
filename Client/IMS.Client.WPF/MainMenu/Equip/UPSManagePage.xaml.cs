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
    /// Interaction logic for UPSManagePage.xaml
    /// </summary>
    public partial class UPSManagePage : Page {
        public EquipPage parent;

        private List<Core.Ups.Info> copyList;
        private List<Core.Ups.Info> addedList = new List<Core.Ups.Info>();
        private List<Core.Ups.Info> changedList = new List<Core.Ups.Info>();

        private List<int> groupCopyList = new List<int>();
        private List<int> panelCopyList = new List<int>();

        public UPSManagePage()
        {
            InitializeComponent();

            Refresh();
        }

        public void Refresh()
        {
            addedList.Clear();
            changedList.Clear();

            copyList = new List<Core.Ups.Info>(Core.DataManager.inst.GetUpsData());
            UPSList.ItemsSource = copyList;

            groupCopyList.Clear();
            var groupInfoList = Core.DataManager.inst.GetGroupData();
            foreach (var groupInfo in groupInfoList) {
                groupCopyList.Add(groupInfo.groupID);
            }

            GroupID.ItemsSource = groupCopyList;

            panelCopyList.Clear();
            var panelInfoList = Core.DataManager.inst.GetPanelData();
            foreach(var panelInfo in panelInfoList) {
                panelCopyList.Add(panelInfo.panelID);
            }

            PanelID.ItemsSource = panelCopyList;

            ResetView();
        }

        private void ResetView()
        {
            var view = CollectionViewSource.GetDefaultView(UPSList.ItemsSource) as ListCollectionView;
            if (view == null) {
                return;
            }

            if (view.IsAddingNew == true) {
                view.CommitNew();
            }
            if (view.IsEditingItem == true) {
                view.CommitEdit();
            }

            if (view != null && view.SortDescriptions != null) {
                view.SortDescriptions.Clear();
                foreach (var column in UPSList.Columns) {
                    column.SortDirection = null;
                }
            }

            UPSList.Items.Refresh();
        }

        private void AdditionalInformation_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual) {
                if (vis is DataGridRow) {
                    var row = vis as DataGridRow;
                    var info = row.DataContext as Core.Ups.Info;

                    parent.UpsInfoPopup(info.upsID);

                    break;
                }
            }
        }

        private void UPSList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var info = UPSList.SelectedItem as Core.Ups.Info;

            if (e.Column.Header.ToString() == "짝") {
                var txt = e.EditingElement as TextBox;
                if (txt.Text != "") {
                    info.partnerList = Core.IntList.Parse(txt.Text);
                }
            }

            if (addedList.Contains(info) == false && changedList.Contains(info) == false) {
                changedList.Add(info);
            }
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            foreach (var info in addedList) {
                Core.DataManager.inst.AddUps(info);
            }

            foreach (var info in changedList) {
                Core.DataManager.inst.EditUps(info);
            }

            parent.UpsRefresh();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var newInfo = new Core.Ups.Info();
            addedList.Add(newInfo);
            copyList.Add(newInfo);

            ResetView();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            var info = UPSList.SelectedItem as Core.Ups.Info;
            if (addedList.Contains(info) == true) {
                addedList.Remove(info);
                copyList.Remove(info);

                ResetView();
            }
            else {
                var result = MessageBox.Show("삭제하시겠습니까?  삭제는 바로 적용됩니다.", "", MessageBoxButton.YesNoCancel);
                switch (result) {
                    case MessageBoxResult.Yes: {
                            Core.DataManager.inst.DeleteUps(info.upsID);
                            parent.UpsRefreshExceptUps();

                            copyList.Remove(info);
                            ResetView();
                        }
                        break;
                }
            }
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void UPSList_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(UPSList.ItemsSource) as ListCollectionView;
            if (view.IsAddingNew == true || view.IsEditingItem == true) {
                UPSList.CanUserSortColumns = false;
            }
            else {
                UPSList.CanUserSortColumns = true;
            }
        }
    }
}