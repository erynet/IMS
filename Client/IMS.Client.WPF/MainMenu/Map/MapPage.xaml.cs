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
        private List<System.Windows.Controls.Image> groupImageList = new List<System.Windows.Controls.Image>();

        public static string NormalIcon = "group_icon_normal_1.png";
        public static string AlarmIcon = "group_icon_alarm_1.png";

        public MapPage()
        {
            InitializeComponent();

            var groupList = Core.Client.inst.GetGroupData();
            foreach (var data in groupList) {
                var image = new System.Windows.Controls.Image();
                image.Source = BitmapToImageSource(Properties.Resources.group_icon_normal_1);
                image.Margin = new Thickness(data.coordinate.X, data.coordinate.Y, 0, 0);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Top;
                image.Width = 64;
                image.Height = 64;

                var grid = Content as Grid;
                grid.Children.Add(image);
            }
        }

        BitmapImage BitmapToImageSource(Bitmap bitmap)
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
