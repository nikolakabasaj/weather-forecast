using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_forecast.Interfaces;
using Weather_forecast.JSONMapper;

namespace Weather_forecast.Models
{
    public class Forecast : IForecast
    {
        public static Dictionary<string, LocationForecast> forecasts;
        public static Mapper mapper;

        static Forecast()
        {
            forecasts = new Dictionary<string, LocationForecast>();
            mapper = new Mapper();
        }

        public LocationForecast getLocationForecast(string cityName)
        {
            LocationForecast lf = mapper.getLocationForecast(cityName);
            forecasts[lf.Name] = lf;
            return lf;
        }
    }
}
