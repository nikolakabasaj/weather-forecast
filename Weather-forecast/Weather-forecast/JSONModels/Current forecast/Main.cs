using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_forecast.JSONModels.Current_forecast
{
    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }

        public double getCelsius()
        {
            return Math.Round(temp - 273.15, 1);
        }
    }
}
