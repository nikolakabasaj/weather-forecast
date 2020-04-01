using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_forecast.Models
{
    public class LocationDailyWeather
    {
        public DateTime Time { get; set; }
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Pressure { get; set; }
        public int Sea_level { get; set; }
        public int Ground_level { get; set; }
        public int Humidity { get; set; }
        public double Temp_kf { get; set; }
        public int Cloud_all { get; set; }
        public double Wind_speed { get; set; }
        public double Wind_degree { get; set; }
        public string Icon { get; set; }

        public LocationDailyWeather(DateTime time, double temp, double feels_like, double temp_min, double temp_max, int pressure,
            int sea_level, int ground_level, int humidity, double temp_kf, int cloud_all, double speed, double degree, string icon)
        {
            Time = time;
            Temp = temp;
            Feels_like = feels_like;
            Temp_min = temp_min;
            Temp_max = temp_max;
            Pressure = pressure;
            Sea_level = sea_level;
            Ground_level = ground_level;
            Humidity = humidity;
            Temp_kf = temp_kf;
            Cloud_all = cloud_all;
            Wind_speed = speed;
            Wind_degree = degree;
            Icon = icon;
        }
    }
}
