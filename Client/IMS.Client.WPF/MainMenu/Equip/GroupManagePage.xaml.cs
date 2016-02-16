using System.Windows.Controls;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for GroupManagePage.xaml
    /// </summary>
    public partial class GroupManagePage : Page {
        public GroupManagePage()
        {
            InitializeComponent();

            var groupList = Core.Client.inst.GetGroupData();
            GroupList.ItemsSource = groupList;
        }
    }
}
