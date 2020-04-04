using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using Weather_forecast.Utility;

namespace Weather_forecast.Models
{
    public class LocationDailyWeather
    {
        public DateTime Time { get; set; }
        public double Kelvin { get; set; }
        public double Celsius { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public double Pressure { get; set; }
        public int Sea_level { get; set; }
        public int Humidity { get; set; }
        public int Cloud_all { get; set; }
        public double Wind_speed { get; set; }
        public double Wind_degree { get; set; }
        public Image Icon { get; set; }

        public LocationDailyWeather(DateTime time, double temp, double temp_min, double temp_max, double pressure,
            int sea_level, int humidity, int cloud_all, double wind_speed, double wind_degree, string icon)
        {
            Time = time;
            Kelvin = temp;
            Temp_min = temp_min;
            Temp_max = temp_max;
            Pressure = pressure;
            Sea_level = sea_level;
            Humidity = humidity;
            Cloud_all = cloud_all;
            Wind_speed = wind_speed;
            Wind_degree = wind_degree;
            Icon = IconMaker.LoadImage(icon);
            Celsius =  Math.Round(Kelvin - 273.15, 1);
        }
        public string getOnlyTime()
        {
            return Time.ToString("dd:MM hh:mm tt");
        }

        public string getOnlyDayAndTime()
        {
            return Time.ToString("ddd dd:MM tt");
        }
    }
}
