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
using Cdu = IMS.Client.Core.Data.Cdu;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for PannelManagePage.xaml
    /// </summary>
    public partial class PanelManagePage : Page {
        public EquipPage parent;

        private List<Cdu.Info> copyList;
        private List<Cdu.Info> addedList = new List<Cdu.Info>();
        private List<Cdu.Info> changedList = new List<Cdu.Info>();

        public PanelManagePage()
        {
            InitializeComponent();

            Refresh();
        }

        public void Refresh()
        {
            addedList.Clear();
            changedList.Clear();

            copyList = new List<Cdu.Info>(DataManager.inst.GetCduData());
            PanelList.ItemsSource = copyList;

            ResetView();
        }

        private void ResetView()
        {
            var view = CollectionViewSource.GetDefaultView(PanelList.ItemsSource) as ListCollectionView;
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
                foreach (var column in PanelList.Columns) {
                    column.SortDirection = null;
                }
            }

            PanelList.Items.Refresh();
        }

        private int GetLargestNo()
        {
            int ret = -1;
            foreach (var info in copyList) {
                if (ret == -1) {
                    ret = info.cduNo;
                }
                else {
                    ret = Math.Max(ret, info.cduNo);
                }
            }

            return ret;
        }

        private void ManageDot_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual) {
                if (vis is DataGridRow) {
                    var row = vis as DataGridRow;
                    var info = row.DataContext as Cdu.Info;

                    parent.DotManagePopup(info.cduIdx);

                    break;
                }
            }
        }

        private void AdditionalInformation_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual) {
                if (vis is DataGridRow) {
                    var row = vis as DataGridRow;
                    var info = row.DataContext as Cdu.Info;

                    break;
                }
            }
        }

        private void PanelList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var info = PanelList.SelectedItem as Cdu.Info;

            if (addedList.Contains(info) == false && changedList.Contains(info) == false) {
                changedList.Add(info);
            }
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            // Check
            bool noBelowOne = false;

            foreach (var info in addedList) {
                if (info.cduNo <= 0) {
                    noBelowOne = true;
                    break;
                }
            }

            foreach (var info in changedList) {
                if (info.cduNo <= 0) {
                    noBelowOne = true;
                    break;
                }
            }

            if (noBelowOne == true) {
                MessageBox.Show("장비 번호는 1 이상만 가능합니다.");
                return;
            }

            //Save
            foreach (var info in addedList) {
                DataManager.inst.AddCdu(info);
            }

            foreach (var info in changedList) {
                DataManager.inst.EditCdu(info);
            }

            parent.PanelRefresh();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var newInfo = new Cdu.Info();
            newInfo.cduNo = GetLargestNo() + 1;

            addedList.Add(newInfo);
            copyList.Add(newInfo);

            ResetView();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            bool showMessage = false;
            foreach (var item in PanelList.SelectedItems) {
                var info = item as Cdu.Info;

                if (addedList.Contains(info) == false) {
                    showMessage = true;
                    break;
                }
            }

            if (showMessage == true) {
                var result = MessageBox.Show("삭제하시겠습니까?  삭제는 바로 적용됩니다.", "", MessageBoxButton.YesNoCancel);

                switch (result) {
                    case MessageBoxResult.Yes: {
                            foreach (var item in PanelList.SelectedItems) {
                                var info = item as Cdu.Info;

                                if (addedList.Contains(info) == true) {
                                    addedList.Remove(info);
                                }
                                else {
                                    DataManager.inst.DeleteCdu(info.cduIdx);
                                }

                                copyList.Remove(info);
                            }

                            parent.PanelRefreshExceptPanel();
                            ResetView();
                        }
                        break;
                }
            }
            else {
                foreach (var item in PanelList.SelectedItems) {
                    var info = item as Cdu.Info;

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

        private void PanelList_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(PanelList.ItemsSource) as ListCollectionView;
            if (view.IsAddingNew == true || view.IsEditingItem == true) {
                PanelList.CanUserSortColumns = false;
            }
            else {
                PanelList.CanUserSortColumns = true;
            }
        }
    }
}
