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

        public MainWindow()
        {
            InitializeComponent();
            clock = new Clock(clockTicker);
            CurrentCity c = new CurrentCity();
           
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
            string str = "05-04-2020";
            DateTime date = DateTime.ParseExact(str, "dd-MM-yyyy ", null);
            double tmp = lf.ForecastDict[date].Temp;
            int a = 2;
        }
    }
}
