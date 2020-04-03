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
        public IPLocation location;
        public const string AccessKey = "61f67e137eeaa6d4b07718a0e2873a64";
        public CurrentCity()
        {
            hostName = Dns.GetHostName();
            IP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            location = GetIPLocation(IP);

        }

        public IPLocation GetIPLocation(string IPAddress)
        {
            IPLocation IPLocation;
            string retJson = DownloadDataNoAuth(string.Format("http://api.ipstack.com/{0}?access_key={1}", IPAddress, AccessKey));
            var JO = JObject.Parse(retJson);
            IPLocation = new IPLocation();
            IPLocation.IPAddress = IPAddress;
            IPLocation.CountryCode = (string) JO["country_code"];
            IPLocation.CountryName = (string)JO["country_name"];
            IPLocation.RegionCode = (string)JO["region_code"];
            IPLocation.Region = (string)JO["region_name"];
            IPLocation.CityName = (string)JO["city"];
            IPLocation.ZipCode = (string)JO["zip_code"];
            IPLocation.Latitude = (string)JO["latitude"];
            IPLocation.Longitude = (string)JO["longitude"];
            return IPLocation;
        }

        public string DownloadDataNoAuth(string hostURI)
        {
            string retXml = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(hostURI);
                request.Method = "GET";
                String responseLine = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(dataStream);
                        retXml = sr.ReadToEnd();
                        sr.Close();
                        dataStream.Close();
                    }
                }
            }
            catch (Exception e)
            {
                retXml = null;
            }
            return retXml;
        }


    }

    public class IPLocation
    {
        public string IPAddress { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string Region { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string ISP { get; set; }
        public string Organization { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ZipCode { get; set; }
        public string TimeZone { get; set; }

    }
}
