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
        public class USPInfo {
            public bool isUsing { get; set; }
            public int groupNumber { get; set; }
            public bool isGroupVisible { get; set; }
            public string groupName { get; set; }
            public bool isSeparatelyUseable { get; set; }
            public int upsID { get; set; }
            public string partnerIDs { get; set; }
            public string upsName { get; set; }
            public string batteryCapacity { get; set; }
            public string batteryDescription { get; set; }
            public string ip { get; set; }
            public string installDate { get; set; }
            public string coordinate { get; set; }
        }

        public List<USPInfo> upsList { get; set; }

        public UPSManagePage()
        {
            InitializeComponent();

            upsList = new List<USPInfo>();

            upsList.Add(new USPInfo {
                isUsing = false,
                groupNumber = 0,
                isGroupVisible = true,
                groupName = "화장실",
                isSeparatelyUseable = true,
                upsID = 1,
                partnerIDs = "2",
                upsName = "화장실-1",
                batteryCapacity = "1kW",
                batteryDescription = "듀라셀",
                ip = "192.168.0.1",
                installDate = "2016.01.01",
                coordinate = "200, 300"
            }); upsList.Add(new USPInfo {
                isUsing = false,
                groupNumber = 0,
                isGroupVisible = true,
                groupName = "화장실",
                isSeparatelyUseable = true,
                upsID = 1,
                partnerIDs = "2",
                upsName = "화장실-1",
                batteryCapacity = "1kW",
                batteryDescription = "듀라셀",
                ip = "192.168.0.1",
                installDate = "2016.01.01",
                coordinate = "200, 300"
            });

            UPSList.ItemsSource = upsList;
        }
    }
}
