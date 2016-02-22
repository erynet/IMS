using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using IMS.Client.Core.Data;
using Point = IMS.Client.Core.Data.Point;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for GroupManagePage.xaml
    /// </summary>
    public partial class GroupManagePage : Page {
        public EquipPage parent;

        private List<Group.Info> copyList;
        private List<Group.Info> addedList = new List<Group.Info>();
        private List<Group.Info> changedList = new List<Group.Info>();

        public GroupManagePage()
        {
            InitializeComponent();

            Refresh();
        }

        public void Refresh()
        {
            addedList.Clear();
            changedList.Clear();

            copyList = new List<Group.Info>(DataManager.inst.GetGroupData());
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

        private void GroupList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var info = GroupList.SelectedItem as Group.Info;

            if (e.Column.Header.ToString() == "좌표") {
                var txt = e.EditingElement as TextBox;
                if (txt.Text != "") {
                    info.coordinate = Point.Parse(txt.Text);
                }
            }

            if (addedList.Contains(info) == false && changedList.Contains(info) == false) {
                changedList.Add(info);
            }
        }

        private void button_apply_Click(object sender, RoutedEventArgs e)
        {
            foreach (var info in addedList) {
                DataManager.inst.AddGroup(info);
            }

            foreach (var info in changedList) {
                DataManager.inst.EditGroup(info);
            }

            parent.GroupRefresh();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            var newInfo = new Group.Info();
            addedList.Add(newInfo);
            copyList.Add(newInfo);

            ResetView();
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            bool upsExists = false;
            bool showMessage = false;

            foreach (var item in GroupList.SelectedItems) {
                var info = item as Group.Info;

                if(info.upsIdxList.Count != 0) {
                    upsExists = true;
                }

                if (addedList.Contains(info) == false) {
                    showMessage = true;
                }
            }

            if(upsExists == true) {
                MessageBox.Show("그룹에 소속된 장비가 한 개라도 존재할 경우 삭제할 수 없습니다.", "", MessageBoxButton.OK);
                return;
            }

            if (showMessage == true) {
                var result = MessageBox.Show("삭제하시겠습니까?  삭제는 바로 적용됩니다.", "", MessageBoxButton.YesNoCancel);

                switch (result) {
                    case MessageBoxResult.Yes: {
                            foreach (var item in GroupList.SelectedItems) {
                                var info = item as Group.Info;

                                if (addedList.Contains(info) == true) {
                                    addedList.Remove(info);
                                }
                                else {
                                    DataManager.inst.DeleteGroup(info.groupIdx);
                                }

                                copyList.Remove(info);
                            }

                            parent.GroupRefreshExceptGroup();
                            ResetView();
                        }
                        break;
                }
            }
            else {
                foreach (var item in GroupList.SelectedItems) {
                    var info = item as Group.Info;

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
