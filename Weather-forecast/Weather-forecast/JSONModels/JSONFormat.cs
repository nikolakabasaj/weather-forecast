using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_forecast.OriginalModels;

namespace Weather_forecast.Models
{
    public class JSONFormat
    {
        public string cod;
        public string message;
        public string cnt;
        public List<HourlyWeather> list;
        public City city;
    }
}
