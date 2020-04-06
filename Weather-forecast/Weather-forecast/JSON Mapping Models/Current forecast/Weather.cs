﻿using System.Windows.Controls;
using Weather_forecast.Utility;

namespace Weather_forecast.Models_JSON
{
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

        public Image getIcon()
        {
            return IconMaker.LoadImage(icon);
        }
    }
}