using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for GroupManagePage.xaml
    /// </summary>
    public partial class GroupManagePage : Page {
        public EquipPage parent;

        private List<Core.Group.Info> copyList;
        private List<Core.Group.Info> addedList = new List<Core.Group.Info>();
        private List<Core.Group.Info> changedList = new List<Core.Group.Info>();

        public GroupManagePage()
        {
            InitializeComponent();

            Refresh();
        }

        public void Refresh()
        {
            addedList.Clear();
            changedList.Clear();

            copyList = new List<Core.Group.Info>(Core.Client.inst.GetGroupData());
            GroupList.ItemsSource = copyList;

            ResetView();
        }

        private void ResetView()
        {
            var view = CollectionViewSource.GetDefaultView(GroupList.ItemsSource) as ListCollectionView;
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
                foreach (var column in GroupList.Columns) {
                    column.SortDirection = null;
                }
            }

            GroupList.Items.Refresh();
        }

        private void add_group_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual) {
                if (vis is DataGridRow) {
                    var row = vis as DataGridRow;
                    var info = row.DataContext as Core.Group.Info;

                    parent.GroupPopupCreate();

                    break;
                }
            }
        }

        private void edit_group_Click(object sender, RoutedEventArgs e)
        {
            var info = GroupList.SelectedItem as Core.Group.Info;
            parent.GroupPopupEdit(info.groupNumber);
        }

        private void delete_group_Click(object sender, RoutedEventArgs e)
        {
            var info = GroupList.SelectedItem as Core.Group.Info;

            Core.Client.inst.DeleteGroup(info.groupNumber);
            Refresh();
        }

        private void GroupList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var info = GroupList.SelectedItem as Core.Group.Info;

            if (e.Column.Header.ToString() == "좌표") {
                var txt = e.EditingElement as TextBox;
                if(txt.Text != "") {
                    info.coordinate = Core.Point.Parse(txt.Text);
                }
            }

            if (addedList.Contains(info) == false && changedList.Contains(info) == false) {
                changedList.Add(info);
            }
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            foreach (var info in addedList) {
                Core.Client.inst.AddGroup(info);
            }

            foreach (var info in changedList) {
                Core.Client.inst.EditGroup(info);
            }

            parent.GroupRefresh();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var newInfo = new Core.Group.Info();
            addedList.Add(newInfo);
            copyList.Add(newInfo);

            ResetView();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            var info = GroupList.SelectedItem as Core.Group.Info;
            if (addedList.Contains(info) == true) {
                addedList.Remove(info);
                copyList.Remove(info);

                ResetView();
            }
            else {
                var result = MessageBox.Show("삭제하시겠습니까?  삭제는 바로 적용됩니다.", "", MessageBoxButton.YesNoCancel);
                switch (result) {
                    case MessageBoxResult.Yes: {
                            Core.Client.inst.DeleteGroup(info.groupNumber);
                            parent.GroupRefreshExceptGroup();

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

        private void GroupList_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var view = CollectionViewSource.GetDefaultView(GroupList.ItemsSource) as ListCollectionView;
            if (view.IsAddingNew == true || view.IsEditingItem == true) {
                GroupList.CanUserSortColumns = false;
            }
            else {
                GroupList.CanUserSortColumns = true;
            }
        }
    }
}
