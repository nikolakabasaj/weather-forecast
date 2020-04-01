using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_forecast.OriginalModels
{
    public class HourlyWeather
    {
        public double dt;
        public Main main;
        public List<WeatherItem> weather;
        public Clouds cloud;
        public Wind wind;
        public Sys sys;
        public string dt_text;
    }

    public struct Main {
        public double temp;
        public double feels_like;
        public double temp_min;
        public double temp_max;
        public int pressure;
        public int sea_level;
        public int grnd_level;
        public int huminidy;
        public double temp_kf;
    }

    public struct WeatherItem
    {   
        public int id;
        public string main;
        public string description;
        public string icon;
    } 

    public struct Clouds
    {
        public int all;
    }

    public struct Wind
    {
        public double speed;
        public double deg;
    }

    public struct Sys
    {
        public string pod;
    }

}
