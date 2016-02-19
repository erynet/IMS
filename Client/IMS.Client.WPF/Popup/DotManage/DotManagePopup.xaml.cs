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
using IMS.Client.Core;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for DotManagePopup.xaml
    /// </summary>
    public partial class DotManagePopup : Window {
        public DotManagePopup()
        {
            InitializeComponent();
        }

        public void Refresh(int panelID)
        {
            var panel = DataManager.inst.GetPanel(panelID);
            if (panel == null) {
                return;
            }


            if(panel.Data.isExtended == true) {
                ExtendedDotList.Visibility = Visibility.Visible;
            }
            else {
                ExtendedDotList.Visibility = Visibility.Hidden;
            }
        }
    }
}
