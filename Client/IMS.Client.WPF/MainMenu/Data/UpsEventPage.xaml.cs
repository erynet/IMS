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
using IMS.Client.Core.Data;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for UpsEventPage.xaml
    /// </summary>
    public partial class UpsEventPage : Page {
        public UpsEventPage()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            var upsEventList = DataManager.inst.GetRecentUpsEvent(0, 100);
            UpsEventList.ItemsSource = upsEventList;
        }
    }
}
