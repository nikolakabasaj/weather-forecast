using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_forecast.Models;

namespace Weather_forecast.Interfaces
{
    interface IForecast
    {
        LocationDailyWeather getFirstLocationDailyForecast();
        List<LocationDailyWeather> getFirstFivePrognosis(string cityName);
        //List<LocationDailyWeather> getForecastInDaterange();
        LocationDailyWeather getForecastForDatetime(DateTime datetime);
    }
}
