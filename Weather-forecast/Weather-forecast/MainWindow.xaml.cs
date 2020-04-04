using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Weather_forecast.JSONMapper;
using Weather_forecast.Models;
using Weather_forecast.Utility;
using Weather_forecast.Web_mapping;

namespace Weather_forecast
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Forecast forecast = new Forecast();
        public Clock clock;
        CurrentCity city = new CurrentCity();
        LocationForecast currentCityForecast = forecast.getLocationForecast("Novi Sad");

        public MainWindow()
        {
            InitializeComponent();
            clock = new Clock(clockTicker);
            currentCity.Content = currentCityForecast.Name;
            temmCurrentLocation.Content = currentCityForecast.getFirstValue().Celsius;
            icon.Content = currentCityForecast.getFirstValue().Icon;
            measuredTime.Content = currentCityForecast.getFirstValue().getOnlyTime();
        }

        public void clockTicker(object sender, EventArgs e)
        {   
            clock.tickIncrement++;
            string[] values = clock.getCurrentTime();
            string hoursMinutes = values[0] + ":" + values[1];

            clockTime.Content = hoursMinutes;
            clockSeconds.Content = values[2];
        }
        private void tableView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void graphView_Click(object sender, RoutedEventArgs e)
        {

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string s = searchText.Text;
            forecast.storeLocationForecast(searchText.Text);
            LocationForecast lf = forecast.getLocationForecast(searchText.Text);
            string str = "05/04/2020 11:00 AM";
            DateTime date = DateTime.ParseExact(str, "dd/MM/yyyy hh:mm tt", null);
            double tmp = lf.ForecastDict[date].Celsius;
        }
    }
}
