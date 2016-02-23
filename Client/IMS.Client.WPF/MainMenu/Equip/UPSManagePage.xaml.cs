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
using IMS.Client.Core.Data;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for UPSManagePage.xaml
    /// </summary>
    public partial class UPSManagePage : Page {
        public EquipPage parent;

        private List<Ups.Info> copyList;
        private List<Ups.Info> addedList = new List<Ups.Info>();
        private List<Ups.Info> changedList = new List<Ups.Info>();

        private List<int> groupCopyList = new List<int>();
        private List<int> panelCopyList = new List<int>();

        public UPSManagePage()
        {
            InitializeComponent();

            Refresh();
        }

        public void Refresh()
        {
            ResetView();

            addedList.Clear();
            changedList.Clear();

            copyList = new List<Ups.Info>(DataManager.inst.GetUpsData());
            UPSList.ItemsSource = copyList;

            groupCopyList.Clear();
            var groupInfoList = DataManager.inst.GetGroupData();
            foreach (var groupInfo in groupInfoList) {
                groupCopyList.Add(groupInfo.groupNo);
            }

            GroupNo.ItemsSource = groupCopyList;

            panelCopyList.Clear();
            var panelInfoList = DataManager.inst.GetCduData();
            foreach (var panelInfo in panelInfoList) {
                panelCopyList.Add(panelInfo.cduNo);
            }

            PanelNo.ItemsSource = panelCopyList;

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

        private int GetLargestNo()
        {
            int ret = -1;
            foreach (var info in copyList) {
                if (ret == -1) {
                    ret = info.upsNo;
                }
                else {
                    ret = Math.Max(ret, info.upsNo);
                }
            }

            return ret;
        }

        private void AdditionalInformation_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual) {
                if (vis is DataGridRow) {
                    var row = vis as DataGridRow;
                    var info = row.DataContext as Ups.Info;

                    parent.UpsInfoPopup(info.upsNo);

                    break;
                }
            }
        }

        private void UPSList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var info = UPSList.SelectedItem as Ups.Info;

            if (e.Column.Header.ToString() == "짝") {
                var txt = e.EditingElement as TextBox;
                if (txt.Text != "") {
                    info.partnerIdxList = IntList.Parse(txt.Text);
                }
            }

            if (addedList.Contains(info) == false && changedList.Contains(info) == false) {
                changedList.Add(info);
            }
        }

        private void GroupNoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) {
                return;
            }

            int groupNo = (int)e.AddedItems[0];

            var info = UPSList.SelectedItem as Ups.Info;
            info.groupNo = groupNo;
        }

        private void PanelNoSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) {
                return;
            }

            int panelNo = (int)e.AddedItems[0];

            var info = UPSList.SelectedItem as Ups.Info;
            info.cduNo = panelNo;
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            foreach (var info in addedList) {
                DataManager.inst.AddUps(info);
            }

            foreach (var info in changedList) {
                DataManager.inst.EditUps(info);
            }

            parent.UpsRefresh();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var newInfo = new Ups.Info();
            newInfo.upsNo = GetLargestNo() + 1;

            addedList.Add(newInfo);
            copyList.Add(newInfo);

            ResetView();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            bool showMessage = false;
            foreach (var item in UPSList.SelectedItems) {
                var info = item as Ups.Info;

                if (addedList.Contains(info) == false) {
                    showMessage = true;
                    break;
                }
            }

            if (showMessage == true) {
                var result = MessageBox.Show("삭제하시겠습니까?  삭제는 바로 적용됩니다.", "", MessageBoxButton.YesNoCancel);

                switch (result) {
                    case MessageBoxResult.Yes: {
                            foreach (var item in UPSList.SelectedItems) {
                                var info = item as Ups.Info;

                                if (addedList.Contains(info) == true) {
                                    addedList.Remove(info);
                                }
                                else {
                                    DataManager.inst.DeleteUps(info.upsIdx);
                                }

                                copyList.Remove(info);
                            }

                            parent.UpsRefreshExceptUps();
                            ResetView();
                        }
                        break;
                }
            }
            else {
                foreach (var item in UPSList.SelectedItems) {
                    var info = item as Ups.Info;

                    addedList.Remove(info);
                    copyList.Remove(info);
                }

                ResetView();
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