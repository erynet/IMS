using System.Windows.Controls;
using System.Windows.Media;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for GroupManagePage.xaml
    /// </summary>
    public partial class GroupManagePage : Page {
        public EquipPage parent;

        public GroupManagePage()
        {
            InitializeComponent();

            Refresh();
        }

        public void Refresh()
        {
            var groupList = Core.Client.inst.GetGroupData();
            GroupList.ItemsSource = groupList;
        }

        private void add_group_Click(object sender, System.Windows.RoutedEventArgs e)
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

        private void edit_group_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var info = GroupList.SelectedItem as Core.Group.Info;
            parent.GroupPopupEdit(info.groupNumber);
        }

        private void delete_group_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var info = GroupList.SelectedItem as Core.Group.Info;

            Core.Client.inst.DeleteGroup(info.groupNumber);
            Refresh();
        }

        private void GroupList_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }
    }
}
