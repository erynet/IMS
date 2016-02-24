using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using IMS.Client.Core.Data;

namespace IMS.Client.WPF {
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : Page {
        private class GroupImageDisplay {
            public System.Windows.Controls.Image groupIcon = new System.Windows.Controls.Image();
            public Label groupNo = new Label();
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
                grid.Children.Remove(display.groupNo);
                grid.Children.Remove(display.upsCount);
            }

            groupImageList.Clear();

            var groupList = DataManager.inst.GetGroupData();
            foreach (var data in groupList) {
                if (data.isGroupVisible == false) {
                    continue;
                }

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
                var groupNoLabel = display.groupNo;
                groupNoLabel.Content = data.groupNo;
                groupNoLabel.Margin = new Thickness(data.coordinate.X + 45, data.coordinate.Y, 0, 0);

                // UPS count
                var upsCountLabel = display.upsCount;
                upsCountLabel.Content = data.upsIdxList.Count;
                upsCountLabel.Margin = new Thickness(data.coordinate.X + 45, data.coordinate.Y + 40, 0, 0);

                grid.Children.Add(image);
                grid.Children.Add(groupNoLabel);
                grid.Children.Add(upsCountLabel);
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (var memory = new MemoryStream()) {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
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
