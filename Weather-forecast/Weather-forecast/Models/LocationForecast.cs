using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_forecast.Models_JSON;
using Weather_forecast.Utility;

namespace Weather_forecast.Models
{
    public class LocationForecast
    {
        public string Name { get; set; }

        public Dictionary<DateTime, LocationDailyWeather> ForecastDict { get; set; }
        public void loadIcons()
        {
            foreach (LocationDailyWeather ldw in ForecastDict.Values)
                ldw.Icon = IconHandler.LoadImage(ldw.StringIcon);
        }
        public LocationForecast()
        {
            ForecastDict = new Dictionary<DateTime, LocationDailyWeather>();
        }
        public LocationForecast(string name, Dictionary<DateTime, LocationDailyWeather> forecastDict)
        {
            Name = name;
            ForecastDict = forecastDict;
        }
        public HashSet<DateTime> getAllKeys()
        {
            return ForecastDict.Keys.ToHashSet();
        }
        public LocationDailyWeather getFirstValue()
        {
            return ForecastDict.Values.First();
        }
        public LocationForecast filterDateTime(DateTime from, DateTime to)
        {
            Dictionary<DateTime, LocationDailyWeather> retDict = ForecastDict;
            if (from != null)
                retDict = retDict.Where(i => from <= i.Key).ToDictionary(i => i.Key, i => i.Value);
            if (to != null)
                retDict = retDict.Where(i => i.Key <= to).ToDictionary(i => i.Key, i => i.Value);
            
            return new LocationForecast(Name,retDict);
        }
    }
}
