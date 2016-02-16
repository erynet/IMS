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
        public class PanelInfo {
            public bool isUsing { get; set; }
            public int panelID { get; set; }
            public string panelName { get; set; }
            public bool isExtended { get; set; }
            public string ip { get; set; }
            public string installDate { get; set; }
        }

        private List<PanelInfo> panelList = new List<PanelInfo>();

        public PanelManagePage()
        {
            InitializeComponent();

            panelList.Add(new PanelInfo {
                isUsing = true,
                panelID = 3,
                panelName = "방",
                isExtended = false,
                ip = "192.168.0.1",
                installDate = "2016.01.01",
            });

            PanelList.ItemsSource = panelList;
        }

        private void ManageDot_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual) {
                if (vis is DataGridRow) {
                    var row = vis as DataGridRow;
                    var info = row.DataContext as PanelInfo;

                    break;
                }
            }
        }

        private void AdditionInformation_Click(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual) {
                if (vis is DataGridRow) {
                    var row = vis as DataGridRow;
                    var info = row.DataContext as PanelInfo;

                    break;
                }
            }
        }
    }
}
