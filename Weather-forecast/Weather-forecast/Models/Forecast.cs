using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_forecast.Interfaces;
using Weather_forecast.JSONMapper;
using Weather_forecast.JSONModels.Current_forecast;

namespace Weather_forecast.Models
{
    /* Forecast has dictionary
     * key: Name of location, value: forecast for location
     */
    public class Forecast : IForecast
    {
        public Dictionary<string, LocationForecast> locationForecast;
        public static Mapper mapper;
        public HashSet<DateTime> allDates; 

        public Forecast()
        {
            locationForecast = new Dictionary<string, LocationForecast>();
            mapper = new Mapper();
            allDates = new HashSet<DateTime>();
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

            foreach (LocationDailyWeather ldw in getLocationForecast(cityName).ForecastDict.Values)
            {
                if (cnt == 5) break;
                firstFive.Add(ldw);
                cnt++;
            }
            return firstFive;
        }
        public LocationDailyWeather getForecastForDatetime(DateTime datetime)
        {
            throw new NotImplementedException();
        }
        public CurrentWeather getCurrentWeather(string cityName)
        {
            return mapper.JSONtoCurrentCity(cityName);
        }
        public LocationForecast getLocationForecast(string cityName)
        {
            storeLocationForecast(cityName);
            return locationForecast[cityName.ToLower()];
        }
        public HashSet<DateTime> getDates()
        {
            return new HashSet<DateTime>(); // ovde ce da bude logika dobvljanja datuma, ako ima filter vrednosti i 
        }
        public void storeLocationForecast(string cityName)
        {
            if (!isCityStored(cityName))
            {
                LocationForecast lf = mapper.getLocationForecast(cityName);
                locationForecast.Add(lf.Name.ToLower(), lf);
            }
        }
        public HashSet<LocationForecast> getLocationForecastFilteredName(string cityName)
        {
            Dictionary<string, LocationForecast> filteredDict = filterName(cityName);
            HashSet<LocationForecast> filteredHashSet = new HashSet<LocationForecast>();
            foreach (var lf in filteredDict.Values)
                filteredHashSet.Add(lf);
            return filteredHashSet;
        }
        public HashSet<LocationForecast> getLocationForecastFilteredDate(DateTime from, DateTime to)
        {
            Dictionary<string, LocationForecast> filteredDict = filterDate(from, to);
            HashSet<LocationForecast> filteredHashSet = new HashSet<LocationForecast>();
            foreach (var lf in filteredDict.Values)
                filteredHashSet.Add(lf);
            return filteredHashSet;
        }
        public Dictionary<string, LocationForecast> filterName(string cityName)
        {
            if (locationForecast.ContainsKey(cityName)) 
                return locationForecast;
            return locationForecast.Where(i => i.Key == cityName).ToDictionary(i => i.Key, i => i.Value);
        }
        public Dictionary<string, LocationForecast> filterDate(DateTime from, DateTime to)
        {
            if (isDateRangeValid(from, to)) 
                return locationForecast;

            Dictionary<string, LocationForecast> retForecast = new Dictionary<string, LocationForecast>();
            foreach ( var item in locationForecast)
            {
                retForecast.Add(item.Key, item.Value.filterDateTime(from, to));
            }
            return retForecast;
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
