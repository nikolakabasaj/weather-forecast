﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_forecast.Models;
using Weather_forecast.OriginalModels;
using Weather_forecast.Web_mapping;

namespace Weather_forecast.JSONMapper
{
    public class Mapper
    {
        public static FetchWeather fetchWeather = new FetchWeather();
        
        public JSONFormat getObjectFromJSON(string cityName)
        {
            var json = fetchWeather.getJSONWeather(cityName);
            return JsonConvert.DeserializeObject<JSONFormat>(json);
        }

        public LocationForecast getLocationForecast(string cityName)
        {
            JSONFormat jsonformat = getObjectFromJSON(cityName);
            LocationForecast lf = new LocationForecast();

            lf.Name = jsonformat.city.name;
            foreach (ListObject lo in jsonformat.list)
            {
                lf.ForecastDict[UnixToDatetime.UnixTimeStampToDateTime(lo.dt)] = getSingle(lo);
            }
            return lf;
        }

        public LocationDailyWeather getSingle(ListObject lo)
        {
           return new LocationDailyWeather(UnixToDatetime.UnixTimeStampToDateTime(lo.dt), lo.main.temp, lo.main.feels_like, lo.main.temp_min,
               lo.main.temp_max, lo.main.pressure, lo.main.sea_level, lo.main.grnd_level, lo.main.huminidy, lo.main.temp_kf, lo.cloud.all,
               lo.wind.speed, lo.wind.deg); 
        }
    }
}