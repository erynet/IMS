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
    /// Interaction logic for PannelManagePage.xaml
    /// </summary>
    public partial class PanelManagePage : Page {
        public EquipPage parent;

        private List<Core.Panel.Info> copyList;
        private List<Core.Panel.Info> addedList = new List<Core.Panel.Info>();
        private List<Core.Panel.Info> changedList = new List<Core.Panel.Info>();

        public PanelManagePage()
        {
            InitializeComponent();

            Refresh();
        }

        public void Refresh()
        {
            addedList.Clear();
            changedList.Clear();

            copyList = new List<Core.Panel.Info>(Core.DataManager.inst.GetPanelData());
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

        private void ManageDot_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual) {
                if (vis is DataGridRow) {
                    var row = vis as DataGridRow;
                    var info = row.DataContext as Core.Panel.Info;

                    parent.DotManagePopup(info.panelID);

                    break;
                }
            }
        }

        private void AdditionalInformation_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual) {
                if (vis is DataGridRow) {
                    var row = vis as DataGridRow;
                    var info = row.DataContext as Core.Panel.Info;

                    break;
                }
            }
        }

        private void PanelList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var info = PanelList.SelectedItem as Core.Panel.Info;

            if (addedList.Contains(info) == false && changedList.Contains(info) == false) {
                changedList.Add(info);
            }
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            foreach (var info in addedList) {
                Core.DataManager.inst.AddPanel(info);
            }

            foreach (var info in changedList) {
                Core.DataManager.inst.EditPanel(info);
            }

            parent.PanelRefresh();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var newInfo = new Core.Panel.Info();
            addedList.Add(newInfo);
            copyList.Add(newInfo);

            ResetView();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            var info = PanelList.SelectedItem as Core.Panel.Info;
            if (addedList.Contains(info) == true) {
                addedList.Remove(info);
                copyList.Remove(info);

                ResetView();
            }
            else {
                var result = MessageBox.Show("삭제하시겠습니까?  삭제는 바로 적용됩니다.", "", MessageBoxButton.YesNoCancel);
                switch (result) {
                    case MessageBoxResult.Yes: {
                            Core.DataManager.inst.DeletePanel(info.panelID);
                            parent.PanelRefreshExceptPanel();

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
