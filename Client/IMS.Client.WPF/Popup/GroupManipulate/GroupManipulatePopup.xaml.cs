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
using System.Windows.Shapes;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for GroupManipulatePopup.xaml
    /// </summary>
    public partial class GroupManipulatePopup : Window {
        public enum Mode {
            Create,
            Edit
        }

        private Mode mode;
        private int groupID;
        private EquipPage page;

        public GroupManipulatePopup()
        {
            InitializeComponent();
        }

        public void CreateMode(EquipPage page)
        {
            mode = Mode.Create;
            this.page = page;
        }

        public void EditMode(EquipPage page, int groupID)
        {
            mode = Mode.Edit;
            this.groupID = groupID;
            this.page = page;

            var group = Core.Client.inst.GetGroup(groupID);
            if (group == null) {
                return;
            }

            string str = "";
            foreach (var id in group.Data.UpsList) {
                str += id + ",";
            }

            if (group.Data.UpsList.Count != 0) {
                str = str.Remove(str.Length - 1, 1);
            }

            groupName_text.Text = group.Data.groupName;
            upsList_text.Text = str;
            coordinate_text.Text = group.Data.coordinate.ToString();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var groupName = groupName_text.Text;
            var upsList = upsList_text.Text;
            var coord = coordinate_text.Text;

            if (mode == Mode.Create) {
                Core.Client.inst.AddGroup(groupName, upsList, coord);
            }
            else if (mode == Mode.Edit) {
                Core.Client.inst.EditGroup(groupID, groupName, upsList, coord);
            }

            page.GroupRefresh();
            Close();
        }
    }
}