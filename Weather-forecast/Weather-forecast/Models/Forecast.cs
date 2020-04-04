using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_forecast.Interfaces;
using Weather_forecast.JSONMapper;

namespace Weather_forecast.Models
{
    /* Forecast has dictionary
     * key: Name of location, value: forecast for location
     */
    public class Forecast : IForecast
    {
        public static Dictionary<string, LocationForecast> forecast;
        public static Mapper mapper;

        static Forecast()
        {
            forecast = new Dictionary<string, LocationForecast>();
            mapper = new Mapper();
        }

        public void storeLocationForecast(string cityName)
        {
            if (!cityStored(cityName))
            {
                LocationForecast lf = mapper.getLocationForecast(cityName);
                forecast.Add(lf.Name.ToLower(), lf);
            }
        }
        public LocationForecast getLocationForecast(string cityName)
        {
            storeLocationForecast(cityName);
            return forecast[cityName.ToLower()];
        }

        private bool cityStored(string cityName)
        {
            if (!forecast.ContainsKey(cityName))
                return false;

            return true;
        }

        public LocationDailyWeather getFirstLocationDailyForecast()
        {
            return forecast.Values.First().getFirstValue();
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

       
    }
}
