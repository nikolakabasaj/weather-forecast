using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_forecast.OriginalModels
{
    public class City
    {
        public int id;
        public string name;
        public Coord coord;
        public string country;
        public string population;
        public string timezone;
        public string sunrise;
        public string sunset;
    }

    public struct Coord
    {
        public double lat;
        public double lon;
    }
}
