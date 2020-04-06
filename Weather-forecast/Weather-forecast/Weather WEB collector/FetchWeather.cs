using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weather_forecast.Web_mapping
{
    public class FetchWeather
    {
        private const string API_ID = "32430f5291b13553bb2e0a722c4d4f9e";

        public string getJSONWeather(string cityName) {
            using (WebClient client = new WebClient())
            {
                string url = string.Format("http://"+$"api.openweathermap.org/data/2.5/forecast?q={cityName}&appid={API_ID}");
                var json = client.DownloadString(url);
                return json;
            }
        }

        public string getCurrentWeather(string cityName)
        {
            using (WebClient client = new WebClient())
            {
                string url = string.Format("http://" + $"api.openweathermap.org/data/2.5/weather?q={cityName}&appid={API_ID}");
                var json = client.DownloadString(url);
                return json;
            }
        }
    }
}
