using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using IMS.Client.Core.Data;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for DotManagePopup.xaml
    /// </summary>
    public partial class DotManagePopup : Window {
        private class Data {
            public CheckBox enabled;
            public TextBox name;
        }

        private int cduIdx;
        private Dictionary<int, Data> dataList = new Dictionary<int, Data>();

        public DotManagePopup()
        {
            InitializeComponent();

            for (int i = 0; i < 48; ++i) {
                // _commandCollection is an instance, private member
                BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

                // Retrieve a FieldInfo instance corresponding to the field
                FieldInfo enabledField = GetType().GetField("enable" + (i + 1), flags);
                FieldInfo nameField = GetType().GetField("name" + (i + 1), flags);

                var data = new Data {
                    enabled = enabledField.GetValue(this) as CheckBox,
                    name = nameField.GetValue(this) as TextBox
                };

                dataList.Add(i + 1, data);
            }

        }

        public void Refresh(int panelID)
        {
            var panel = DataManager.inst.GetCdu(panelID);
            if (panel == null) {
                return;
            }

            cduIdx = panelID;

            var socketList = DataManager.inst.GetSocketData(panelID);
            foreach (var socket in socketList) {
                var id = socket.cduSocketNo;
                var name = socket.cduSocketName;
                var enabled = socket.isUsing;

                dataList[id].enabled.IsChecked = enabled;
                dataList[id].name.Text = name;
            }

            if (panel.isExtended == true) {
                ExtendedDotList.Visibility = Visibility.Visible;
            }
            else {
                ExtendedDotList.Visibility = Visibility.Hidden;
            }
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            var list = new List<CduSocket.Info>();

            foreach (var pair in dataList) {
                if (pair.Value.name.Text == "") {
                    continue;
                }

                var socketInfo = new CduSocket.Info();
                socketInfo.cduIdx = cduIdx;
                socketInfo.isUsing = pair.Value.enabled.IsChecked ?? false;
                socketInfo.cduSocketName = pair.Value.name.Text;
                socketInfo.cduSocketNo = pair.Key;

                list.Add(socketInfo);
            }

            DataManager.inst.EditSocket(cduIdx, list);
        }
    }
}
