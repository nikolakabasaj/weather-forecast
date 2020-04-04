using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_forecast.Models_JSON
{
    class JSONObject
    {
        public int cod { get; set; }
        public int message { get; set; }
        public City city { get; set; }
        public int cnt { get; set; }
        public List<DailyForecast> list { get; set; }
    }
}
