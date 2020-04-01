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
            LocationForecast lf = mapper.getLocationForecast(cityName);
            forecast[lf.Name] = lf;
        }
        public LocationForecast getLocationForecast(string cityName)
        {
            return forecast[cityName];
        }
    }
}
