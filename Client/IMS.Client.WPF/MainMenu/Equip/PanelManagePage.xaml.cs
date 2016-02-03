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
            public string panelName { get; set; }
            public string upsList { get; set; }
            public int dotCount { get; set; }
            public string ip { get; set; }
            public string installDate { get; set; }
            public string dotName { get; set; }
        }

        private List<PanelInfo> panelList = new List<PanelInfo>();

        public PanelManagePage()
        {
            InitializeComponent();

            panelList.Add(new PanelInfo {
                isUsing = true,
                panelName = "방",
                upsList = "1, 2, 3",
                dotCount = 3,
                ip = "192.168.0.1",
                installDate = "2016.01.01",
                dotName = "방"
            });

            PanelList.ItemsSource = panelList;
        }
    }
}
