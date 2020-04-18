using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Weather_forecast.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Weather_forecast.XAML_Components.Graph;
using System.Threading;
using System.Windows.Threading;
using Weather_forecast.Utility;

namespace Weather_forecast.Components.Table
{
    /// <summary>
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : Window
    {
        public ObservableCollection<LocationDailyWeather> LocationDaylyForecasts { get; set; }
        private static string[] graphParameters = { "Temperature", "Pressure", "Visibility", "Humidity" };
        private bool _isMessageVisible;
        private DispatcherTimer messageTimer = MainWindow.messageTimer;
       
        private bool IsMessageVisible
        {
            get { 
                return _isMessageVisible;
            }
            set {
                if (_isMessageVisible != value)
                {
                    _isMessageVisible = value;
                    Thread startTick = new Thread(TimerStart);
                    startTick.Start();
                }
            }
        }

        public TableView()
        {
            LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>();
            initializeTableRows();
            initializeAllComponents();
            InitializeComponent();
            setComponentsValues();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Kelvin" || propertyDescriptor.DisplayName == "Icon")
            {
                e.Cancel = true;
            }
        }
        
        private void initializeAllComponents()
        {
            fromDate = new ComboBox();
            toDate = new ComboBox();

            graphType = new ComboBox(); 
        }
        
        private void setComponentsValues()
        {
            fromDate.ItemsSource = MainWindow.forecast.getAllDates();
            toDate.ItemsSource = MainWindow.forecast.getAllDates();

            graphType.ItemsSource = graphParameters;            
            listBox.ItemsSource = MainWindow.forecast.getAllCities(); 

            fromDate.SelectedItem = MainWindow.forecast.getAllDates().First();
            toDate.SelectedItem = MainWindow.forecast.getAllDates().Last();
            graphType.ItemsSource = graphParameters;

            cityToDelete.ItemsSource = MainWindow.forecast.getAllCities();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Nullable<DateTime> from = fromDate.SelectedItem as Nullable<DateTime>;
            Nullable<DateTime> to = toDate.SelectedItem as Nullable<DateTime>;
            string cityName = filterCity.Text.ToLower();

            tableView.ItemsSource = null;
            tableView.ItemsSource = applyFilter(from, to, cityName);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            LocationDaylyForecasts.Clear();
            initializeTableRows();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string cityName = cityToDelete.SelectedItem as string;

            if(cityName != null) {
                cityName = cityName.ToLower();

                tableView.ItemsSource = null;
                tableView.ItemsSource = delete(cityName);
              
                MainWindow.forecast.locationForecast.Remove(cityName);
                cityToDelete.ItemsSource = MainWindow.forecast.getAllCities();
                listBox.ItemsSource = MainWindow.forecast.getAllCities();

                messageInformation.Content = $"{ StringHandler.capitalize(cityName) } is successfully removed from table!";
            }
            else
            {
                messageInformation.Content = "You did not choose city to delete!";
            }

            IsMessageVisible = true;
        }

        private void Graph_View_Click(object sender, RoutedEventArgs e)
        {
            string graphDataType = adjustGraphTypeNames(graphType.SelectedItem as string);
            List<string> listBoxItems = new  List<string>();
            foreach (string s in listBox.SelectedItems)
                listBoxItems.Add(s.ToLower());


            if (graphDataType == "" || listBoxItems.Count == 0)
            {
                messageInformation.Content = "You did not choose values for graph!";
                IsMessageVisible = true;
            }
            else
            {
                GraphView graph = new GraphView();
                graph.setComponents(graphDataType, listBoxItems.ToArray());
                graph.Show();
            }
        }
        
        private ObservableCollection<LocationDailyWeather> delete(string cityName) {
            LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>(LocationDaylyForecasts.Where(i => i.Name != StringHandler.capitalize(cityName)));
            return LocationDaylyForecasts;
        }

        private ObservableCollection<LocationDailyWeather> applyFilter(Nullable<DateTime> from, Nullable<DateTime> to, string cityName)
        {
            if (from != null)
                LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>(LocationDaylyForecasts.Where(i => from <= i.Time));
            if(to != null)
                LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>(LocationDaylyForecasts.Where(i => i.Time <= to)); 
            if (cityName != null && !cityName.Equals(""))
                LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>(LocationDaylyForecasts.Where(i => i.Name.ToLower().Contains(cityName.ToLower())));
            return LocationDaylyForecasts;
        }

        private void TimerStart()
        {
            if (IsMessageVisible)
            {
                Thread.Sleep(2000);
                messageTimer.Interval = TimeSpan.FromMilliseconds(30);
                messageTimer.Tick += dt_Tick;
                messageTimer.Start();
            }
        }

        private void dt_Tick(object sender, object e)
        {
            messageInformation.Opacity -= 0.11;
            if (messageInformation.Opacity <= 0)
            {
                IsMessageVisible = false;
                messageTimer.Stop();

                messageInformation.Opacity = 1.0;
                messageInformation.Content = "";
            }
        }

        private string adjustGraphTypeNames(string typeName)
        {
            string retString = "";
            if (typeName == "Temperature")
            {
                retString = "Celsius";
            }
            else if(typeName == "Visibility")
            {
                retString = "Cloud_all";
            }
            else
            {
                retString = typeName;
            }

            return retString;
        }

        private void initializeTableRows()
        {

            foreach (string key in MainWindow.forecast.locationForecast.Keys)
                addLocationDailyWeatherToForecast(MainWindow.forecast.locationForecast[key]);
        }

        private void addLocationDailyWeatherToForecast(LocationForecast lf)
        {
            foreach (LocationDailyWeather ldw in lf.ForecastDict.Values)
                LocationDaylyForecasts.Add(ldw);
        }
    }
}
