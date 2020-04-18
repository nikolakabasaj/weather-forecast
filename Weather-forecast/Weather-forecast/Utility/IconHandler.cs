using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Weather_forecast.Utility
{
    class IconHandler
    {
        private static byte[] getIcon(string iconString)
        {
            using (WebClient client = new WebClient())
            {
                string url = "http://" + $"openweathermap.org/img/wn/{iconString}@2x.png";
                var picture = client.DownloadData(url);
                return picture;
            }
        }

        public static Image LoadImage(string imageString)
        {
            byte[] imageData = getIcon(imageString);
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            
            var controlsImage = new Image();
            controlsImage.Source = image;

            return controlsImage;
        }

    }
}
