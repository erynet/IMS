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

        public UPSManagePage()
        {
            InitializeComponent();

            var upsList = Core.Client.inst.GetUpsData();
            UPSList.ItemsSource = upsList;
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
    }
}
