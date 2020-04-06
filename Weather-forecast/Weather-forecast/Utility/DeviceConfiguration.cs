using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weather_forecast.Utility
{
    class DeviceConfiguration
    {
        public static string Public_IP { get; set; }

        public DeviceConfiguration()
        {
            Public_IP = setPublicIP();
        }
        private string fetchPublicIPJSON()
        {
            using (WebClient client = new WebClient())
            {
                string url = string.Format("https://api.ipify.org?format=json");
                var json = client.DownloadString(url);
                return json;
            }
        }

        private string setPublicIP()
        {
            string json = fetchPublicIPJSON();
            var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            return jsonDict["ip"];
        }
    }
}
