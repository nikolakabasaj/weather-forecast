using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Weather_forecast.Interfaces;
using Weather_forecast.JSONMapper;
using Weather_forecast.JSONModels.Current_forecast;
using Weather_forecast.Utility;

namespace Weather_forecast.Models
{
    /* Forecast has dictionary
     * key: Name of location, value: forecast for location
     */
    public class Forecast : IForecastCurrentLocation
    {
        public Dictionary<string, LocationForecast> locationForecast { get; set; } 
        public LocationForecast currentCityForecast;
        public static Mapper mapper;
        public HashSet<DateTime> allDates; 

        public Forecast()
        {
            locationForecast = new Dictionary<string, LocationForecast>();
            mapper = new Mapper();
            allDates = new HashSet<DateTime>();
        }
        public HashSet<string> getAllCities()
        {
            return locationForecast.Keys.Select(k => StringHandler.capitalize(k)).ToHashSet<string>();
        }
        public HashSet<DateTime> getAllDates()
        {
            if(locationForecast.Count != 0)
            {
                string first = locationForecast.Keys.First();
                foreach (DateTime dt in locationForecast[first].getAllKeys())
                {
                    allDates.Add(dt);
                }
            }
            return allDates;
        }
        public LocationDailyWeather getFirstLocationDailyForecast()
        {
            return locationForecast.Values.First().getFirstValue();
        }
        public List<LocationDailyWeather> getFirstFivePrognosis(string cityName)
        {
            int cnt = 0;
            List<LocationDailyWeather> firstFive = new List<LocationDailyWeather>();
            currentCityForecast = mapper.getLocationForecast(cityName);
            foreach (LocationDailyWeather ldw in currentCityForecast.ForecastDict.Values)
            {
                if (cnt == 5) break;
                ldw.loadIcon();
                firstFive.Add(ldw);
                cnt++;
            }
            return firstFive;
        }
        public CurrentWeather getCurrentWeather(string cityName)
        {
            return mapper.JSONtoCurrentCity(cityName);
        }
        public LocationForecast getLocationForecast(string cityName)
        {
            string UTFname = storeLocationForecast(cityName);
            return locationForecast[UTFname];
        }
        public string storeLocationForecast(string cityName)
        {
            if (!isCityStored(cityName))
            {
                LocationForecast lf = mapper.getLocationForecast(cityName);
                cityName = lf.Name.ToLower();
                locationForecast.Add(cityName, lf);
            }
            return cityName;
        }
        private bool isCityStored(string cityName)
        {
            if (!locationForecast.ContainsKey(cityName))
                return false;

            return true;
        }
        private bool isDateRangeValid(DateTime from, DateTime to)
        {
            if (from > to)
                return false;
            return true;
        }
    }
}
