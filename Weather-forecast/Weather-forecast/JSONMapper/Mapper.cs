using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_forecast.Models;
using Weather_forecast.OriginalModels;
using Weather_forecast.Web_mapping;
using Weather_forecast.Utility;
using Weather_forecast.JSONModels.Current_forecast;

namespace Weather_forecast.JSONMapper
{
    public class Mapper
    {
        public static FetchWeather fetchWeather = new FetchWeather();
        
        private JSONForecast JSONtoJSONObject(string cityName)
        {
            var json = fetchWeather.getJSONWeather(cityName);
            return JsonConvert.DeserializeObject<JSONForecast>(json);
        }

        public LocationForecast getLocationForecast(string cityName)
        {
            JSONForecast jsonObject = JSONtoJSONObject(cityName);
            LocationForecast lf = new LocationForecast();

            lf.Name = jsonObject.city.name;
            foreach (HourlyWeather lo in jsonObject.list) 
            {
                lf.ForecastDict.Add(UnixToDatetime.UnixTimeStampToDateTime(lo.dt), getSingle(lo));
            }
            return lf;
        }

        public LocationDailyWeather getSingle( HourlyWeather lo)
        {
           return new LocationDailyWeather(UnixToDatetime.UnixTimeStampToDateTime(lo.dt), lo.main.temp, lo.main.temp_min, lo.main.temp_max, lo.main.pressure,
            lo.main.sea_level, lo.main.huminidy, lo.cloud.all, lo.wind.speed, lo.wind.deg, lo.weather[0].icon); 
        }
    
        public CurrentWeather JSONtoCurrentCity(string cityName)
        {
            var json = fetchWeather.getCurrentWeather(cityName);
            return JsonConvert.DeserializeObject<CurrentWeather>(json);
        }
    }
}
