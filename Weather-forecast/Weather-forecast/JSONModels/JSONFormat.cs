using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_forecast.OriginalModels;

namespace Weather_forecast.Models
{
    public class JSONForecast
    {
        public string cod { get; set; }
        public string message { get; set; }
        public string cnt { get; set; }
        public List<HourlyWeather> list { get; set; }
        public City city { get; set; }
    }
}
