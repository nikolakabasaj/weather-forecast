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
        public static string hostName;
        public static string IP;
        public CityCoordinates cityCoordinates;

        public CurrentCity()
        {
            cityCoordinates = new CityCoordinates();
            hostName = Dns.GetHostName();
            IP = Dns.GetHostByName(hostName).AddressList[0].ToString();

        }

        public string getCityName()
        {
            return "Belgrade";
        }


    }
}
