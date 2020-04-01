using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_forecast.Models
{
    public class LocationForecast
    {
        public string Name { get; set; }
        public Dictionary<DateTime, LocationDailyWeather> ForecastDict { get; set; }
        public LocationForecast()
        {
            ForecastDict = new Dictionary<DateTime, LocationDailyWeather>();
        }
    }
}
