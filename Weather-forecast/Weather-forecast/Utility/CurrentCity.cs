using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Weather_forecast.Utility
{
    public class CurrentCity
    {
        private static Dictionary<string, string> jsonDict;
        public string CityName;


        public CurrentCity()
        {
            jsonDict = mapCityInformation();
            setCityName();
        }

        private string fetchCityInformation()
        {
            using (WebClient client = new WebClient())
            {
                string url = string.Format("http://" + $"ip-api.com/json/{DeviceConfiguration.Public_IP}");
                var json = client.DownloadString(url);
                return json;
            }
        }

        private Dictionary<string, string> mapCityInformation()
        {
            string json = fetchCityInformation();
            var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return jsonDict;
        }

       private void setCityName()
        {
            CityName = jsonDict["city"];
        }

        
    }
}
