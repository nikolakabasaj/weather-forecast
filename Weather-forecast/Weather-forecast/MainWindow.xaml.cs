using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using Weather_forecast.Components.Table;
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
        private Clock clock;

        private static CurrentCity currCity = new CurrentCity();                                       
        private static string currentCityName = currCity.CityName;   
        private static CurrentWeather currentCityWeather = forecast.getCurrentWeather(currentCityName);

        public static DispatcherTimer messageTimer = new DispatcherTimer();
        private bool _informationMessage;
        private bool InformationMessage
        {
            get { return _informationMessage; }
            set
            {
                if(_informationMessage != value)
                {
                    _informationMessage = value;
                    Thread startTick = new Thread(TimerStart);
                    startTick.Start();
                }  
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            setHomePage();
        }

        public void clockTicker(object sender, EventArgs e)
        {   
            clock.tickIncrement++;
            string[] values = clock.getCurrentTime();
            string hoursMinutes = values[0] + ":" + values[1];

            clockTime.Content = hoursMinutes;
            clockSeconds.Content = values[2];
        }

        private void dt_Tick(object sender, object e)
        {
            informationLabel.Opacity -= 0.11;
            if (informationLabel.Opacity <= 0)
            {
                InformationMessage = false;
                messageTimer.Stop();

                informationLabel.Opacity = 1.0;
                informationLabel.Content = "";
            }
        }
       
        private void TimerStart()
        {
            if (InformationMessage)
            {
                Thread.Sleep(2000);
                messageTimer.Interval = TimeSpan.FromMilliseconds(30);
                messageTimer.Tick += dt_Tick;
                messageTimer.Start();
            }
        }

        private void tableView_Click(object sender, RoutedEventArgs e)
        {
            if (forecast.locationForecast.Count != 0)
            {
                var table = new TableView();
                table.Show();
            }
            else
            {
                informationLabel.Content = "There is no data in table!";
                InformationMessage = true;
            }  
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string cityName = searchText.Text;
           
            try
            {
                LocationForecast lf = forecast.getLocationForecast(cityName);
                
                searchText.Clear();
                informationLabel.Content = $"The city '{StringHandler.capitalize(lf.Name)}' was added to the table!";
            }
            catch
            {
                if(cityName == "") {
                    informationLabel.Content = "You did not enter a city name!";
                }
                else {
                    informationLabel.Content = $"City '{StringHandler.capitalize(cityName)}'does not exist!";
                }
                
            }
            InformationMessage = true;
        }

        private void setFirstFivePrognosis()
        {
            List<LocationDailyWeather> firstFive = forecast.getFirstFivePrognosis(currentCityName);
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
        
        private void setCurrentCityInformation()
        {
            currentCity.Content = currentCityWeather.name;
            temmCurrentLocation.Content = currentCityWeather.main.getCelsius();
            icon.Content = currentCityWeather.weather[0].getIcon();
            measuredTime.Content = DateTime.Now.ToString("hh:mm tt");
        }
        
        private void setClock()
        {
            clock = new Clock(clockTicker);
        }
        
        private void setHomePage()
        {
            setFirstFivePrognosis();
            setCurrentCityInformation();
            setClock();
        }
    }
}
