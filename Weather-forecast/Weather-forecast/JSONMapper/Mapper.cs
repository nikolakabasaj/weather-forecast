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
using System.Threading;

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

        [STAThread]
        public LocationForecast getLocationForecast(string cityName)
        {
            JSONForecast jsonObject = JSONtoJSONObject(cityName);
            LocationForecast lf = new LocationForecast();

            lf.Name = StringHandler.toUTFString(jsonObject.city.name);
            foreach(var lo in jsonObject.list)
                lf.ForecastDict.Add(UnixToDatetime.UnixTimeStampToDateTime(lo.dt), getSingle(lf.Name, lo));
            
            return lf;
        }
        
        public LocationDailyWeather getSingle(string name, HourlyWeather lo)
        {
           return new LocationDailyWeather(name, UnixToDatetime.UnixTimeStampToDateTime(lo.dt), lo.main.temp, lo.main.temp_min, lo.main.temp_max, lo.main.pressure,
            lo.main.sea_level, lo.main.humidity, lo.clouds.all, lo.wind.speed, lo.wind.deg, lo.weather[0].icon); 
        }
    
        public CurrentWeather JSONtoCurrentCity(string cityName)
        {
            var json = fetchWeather.getCurrentWeather(cityName);
            return JsonConvert.DeserializeObject<CurrentWeather>(json);
        }

    }
}
