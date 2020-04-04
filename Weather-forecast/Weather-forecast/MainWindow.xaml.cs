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
using Weather_forecast.JSONModels.Current_forecast;
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

        // ---------------------- 
        CurrentCity city = new CurrentCity();
        public static string name = "Novi Sad";
        CurrentWeather currentCityWeather = forecast.getCurrentWeather(name);
        // ----------------------

        public MainWindow()
        {
            InitializeComponent();
            clock = new Clock(clockTicker);
            currentCity.Content = currentCityWeather.name;
            temmCurrentLocation.Content = currentCityWeather.main.getCelsius();
            icon.Content = currentCityWeather.weather[0].getIcon();
            measuredTime.Content = DateTime.Now.ToString("hh:mm tt");

            List<LocationDailyWeather> firstFive = forecast.getFirstFivePrognosis(name);
            icon1.Content = firstFive[0].Icon;
            time1.Text = firstFive[0].getOnlyDayAndTime();
            temp1.Text = firstFive[0].Celsius.ToString();
                
            icon2.Content = firstFive[1].Icon;
            time2.Text = firstFive[1].getOnlyDayAndTime();
            temp2.Text = firstFive[1].Celsius.ToString();

            icon3.Content = firstFive[2].Icon;
            time3.Text = firstFive[2].getOnlyDayAndTime();
            temp3.Text = firstFive[2].Celsius.ToString();

            icon4.Content = firstFive[3].Icon;
            time4.Text = firstFive[3].getOnlyDayAndTime();
            temp4.Text = firstFive[3].Celsius.ToString();

            icon5.Content = firstFive[4].Icon;
            time5.Text = firstFive[4].getOnlyDayAndTime();
            temp5.Text = firstFive[4].Celsius.ToString();
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
