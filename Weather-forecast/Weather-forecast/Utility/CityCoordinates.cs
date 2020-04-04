using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_forecast.Utility
{
    public class CityCoordinates
    {
        public static string Lon { get; set; }
        public static string Lat { get; set; }

        public CityCoordinates()
        {
            GetLocationProperty();
        }


        static void GetLocationProperty()
        {
            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

            // Do not suppress prompt, and wait 1000 milliseconds to start.
            watcher.TryStart(false, TimeSpan.FromMilliseconds(1000));

            GeoCoordinate coord = watcher.Position.Location;

            if (coord.IsUnknown != true)
            {
                Lon = coord.Longitude.ToString();
                Lat = coord.Latitude.ToString();
                
            }
            else
            {
                Console.WriteLine("Unknown latitude and longitude.");
            }
        }


    }
}
