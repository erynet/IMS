using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : Page {
        private class GroupImageDisplay {
            public System.Windows.Controls.Image groupIcon = new System.Windows.Controls.Image();
            public Label groupID = new Label();
            public Label upsCount = new Label();
        }

        public MainWindow parent;

        private List<GroupImageDisplay> groupImageList = new List<GroupImageDisplay>();

        public MapPage()
        {
            InitializeComponent();

            Refresh();
        }

        public void Refresh()
        {
            var grid = Content as Grid;

            foreach (var display in groupImageList) {
                grid.Children.Remove(display.groupIcon);
                grid.Children.Remove(display.groupID);
                grid.Children.Remove(display.upsCount);
            }

            groupImageList.Clear();

            var groupList = Core.DataManager.inst.GetGroupData();
            foreach (var data in groupList) {
                var display = new GroupImageDisplay();
                groupImageList.Add(display);

                // Icon
                var image = display.groupIcon;
                image.Source = BitmapToImageSource(Properties.Resources.group_icon_normal_1);
                image.Margin = new Thickness(data.coordinate.X, data.coordinate.Y, 0, 0);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Top;
                image.Width = 64;
                image.Height = 64;

                // Group ID
                var groupIDLabel = display.groupID;
                groupIDLabel.Content = data.groupID;
                groupIDLabel.Margin = new Thickness(data.coordinate.X + 48, data.coordinate.Y, 0, 0);

                // UPS count
                var upsCountLabel = display.upsCount;
                upsCountLabel.Content = data.upsList.Count;
                upsCountLabel.Margin = new Thickness(data.coordinate.X + 48, data.coordinate.Y + 40, 0, 0);

                grid.Children.Add(image);
                grid.Children.Add(groupIDLabel);
                grid.Children.Add(upsCountLabel);
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (var memory = new MemoryStream()) {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
