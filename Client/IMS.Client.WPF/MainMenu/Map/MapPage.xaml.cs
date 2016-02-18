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
        public MainWindow parent;

        private List<System.Windows.Controls.Image> groupImageList = new List<System.Windows.Controls.Image>();

        public MapPage()
        {
            InitializeComponent();

            Refresh();
        }

        public void Refresh()
        {
            var grid = Content as Grid;

            foreach (var image in groupImageList) {
                grid.Children.Remove(image);
            }

            groupImageList.Clear();

            var groupList = Core.DataManager.inst.GetGroupData();
            foreach (var data in groupList) {
                var image = new System.Windows.Controls.Image();
                image.Source = BitmapToImageSource(Properties.Resources.group_icon_normal_1);
                image.Margin = new Thickness(data.coordinate.X, data.coordinate.Y, 0, 0);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Top;
                image.Width = 64;
                image.Height = 64;

                grid.Children.Add(image);
                groupImageList.Add(image);
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
