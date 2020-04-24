﻿using System;
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
using System.Windows.Controls.DataVisualization.Charting;

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
            setLastAddedCityAsSelected();
            setDefaultGraphParameter();
            setChartComponents();
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Kelvin" || propertyDescriptor.DisplayName == "Icon" || propertyDescriptor.DisplayName == "StringIcon")
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
            fillListBox();

            fromDate.SelectedItem = MainWindow.forecast.getAllDates().First();
            toDate.SelectedItem = MainWindow.forecast.getAllDates().Last();
            graphType.ItemsSource = graphParameters;
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Nullable<DateTime> from = fromDate.SelectedItem as Nullable<DateTime>;
            Nullable<DateTime> to = toDate.SelectedItem as Nullable<DateTime>;
            List<string> citiesList = getCities();

            tableView.ItemsSource = null;
            tableView.ItemsSource = applyFilter(from, to, citiesList);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            LocationDaylyForecasts.Clear();
            graphType.ItemsSource = null;
            graphType.ItemsSource = graphParameters;

            initializeTableRows();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            List<string> citiesList = getCities();

            if (citiesList.Count != 0)
            {
                string cityName = "";
                foreach (string city in citiesList)
                {

                    cityName = city.ToLower();

                    tableView.ItemsSource = null;
                    tableView.ItemsSource = delete(cityName);

                    MainWindow.forecast.locationForecast.Remove(cityName);
                    listBox.ItemsSource = MainWindow.forecast.getAllCities();
                }

                string message = "";
                if(citiesList.Count == 1)
                {
                    message = "City is";
                }
                else
                {
                    message = "Cities are";
                }
                messageInformation.Content = $"{message} successfully removed from table!";
                IsMessageVisible = true;
            }
            else
            {
                messageInformation.Content = "You did not choose city to delete!";
                IsMessageVisible = true;
            }
           

        }

        private void Graph_View_Click(object sender, RoutedEventArgs e)
        {
            setChartComponents();
        }
        
        private ObservableCollection<LocationDailyWeather> delete(string cityName) {
            LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>(LocationDaylyForecasts.Where(i => i.Name != StringHandler.capitalize(cityName)));
            return LocationDaylyForecasts;
        }

        private ObservableCollection<LocationDailyWeather> applyFilter(Nullable<DateTime> from, Nullable<DateTime> to, List<string> cities)
        {
            if(cities != null)
            {
                foreach (string city in cities)
                    LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>(LocationDaylyForecasts.Where(i => cities.Any(c => c == i.Name)));
            }
            if (from != null)
                LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>(LocationDaylyForecasts.Where(i => from <= i.Time));
            if(to != null)
                LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>(LocationDaylyForecasts.Where(i => i.Time <= to)); 
            if(from != null && to != null && from > to)
                messageInformation.Content = "You have selected invalid values ​​for filtering!";
                IsMessageVisible = true;

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

        public void initializeTableRows()
        {

            foreach (string key in MainWindow.forecast.locationForecast.Keys)
                addLocationDailyWeatherToForecast(MainWindow.forecast.locationForecast[key]);
        }

        private void addLocationDailyWeatherToForecast(LocationForecast lf)
        {
            foreach (LocationDailyWeather ldw in lf.ForecastDict.Values)
                LocationDaylyForecasts.Add(ldw);
        }

        private List<string> getCities()
        {
            List<string> citiesList = new List<string>();
            foreach (string s in listBox.SelectedItems)
                citiesList.Add(s);

            return citiesList;
        }
        
        /*
         * Help methods
        */
        public void fillListBox()
        {
            listBox.ItemsSource = MainWindow.forecast.getAllCities();
        }

        private void setLastAddedCityAsSelected()
        {
            listBox.SelectedItem = MainWindow.forecast.getAllCities().Last();
        }
        
        private void setDefaultGraphParameter()
        {
            graphType.SelectedItem = graphParameters[0];
        }

        private void setChartComponents()
        {
            string graphDataType = adjustGraphTypeNames(graphType.SelectedItem as string);
            List<string> listBoxItems = new List<string>();
            foreach (string s in listBox.SelectedItems)
                if (!listBoxItems.Contains(s))
                    listBoxItems.Add(s.ToLower());

            if (graphDataType == "" || graphDataType == null)
            {
                messageInformation.Content = "You did not choose parameters for graph view!";
                IsMessageVisible = true;
            }
            else if (listBoxItems.Count == 0)
            {
                messageInformation.Content = "You did not choose city for graph view!";
                IsMessageVisible = true;
            }
            else
            {
                setComponents(graphDataType, listBoxItems.ToArray());
            }
        }

        private string adjustGraphTypeNames(string typeName)
        {
            string retString = "";
            if (typeName == "Temperature")
            {
                retString = "Celsius";
            }
            else if (typeName == "Visibility")
            {
                retString = "Cloud_all";
            }
            else
            {
                retString = typeName;
            }

            return retString;
        }

        /*
            Chart view
         */
        public void setComponents(string graphData, string[] cities)
        {
            chartView.Series.Clear();
            setAxesLabels("Forecast during days", $"City {adjustAttributeName(graphData).ToLower()} during days");
            foreach (string city in cities)
            {
                var locationForecast = MainWindow.forecast.locationForecast[city];
                KeyValuePair<string, double>[] valueList = locationForecast.ForecastDict
                                                        .Select(pair => new KeyValuePair<string, double>(pair.Key.ToString("ddd h tt"), getAttributeValueByName(graphData, pair.Value)))
                                                        .ToArray();
                LineSeries lineSeries = new LineSeries();
                lineSeries.Title = city.ToUpper();
                lineSeries.DependentValuePath = "Value";
                lineSeries.IndependentValuePath = "Key";
                lineSeries.ItemsSource = valueList;
                chartView.Series.Add(lineSeries);
            }
        }

        private double getAttributeValueByName(string attributeName, LocationDailyWeather ldw)
        {
            double retVal = 0;
            if (attributeName == "Celsius")
            {
                retVal = ldw.Celsius;

            }
            else if (attributeName == "Pressure")
            {
                retVal = ldw.Pressure;
            }
            else if (attributeName == "Cloud_all")
            {
                retVal = ldw.Cloud_all;
            }
            else if (attributeName == "Humidity")
            {
                retVal = ldw.Humidity;
            }

            return retVal;
        }

        private string adjustAttributeName(string attributeName)
        {
            string retVal = "";
            if (attributeName == "Celsius")
            {
                retVal = "Temperature";

            }
            else if (attributeName == "Cloud_all")
            {
                retVal = "Visibility";
            }
            else
            {
                retVal = attributeName;
            }

            return retVal;
        }

        private void setAxesLabels(string xAxesName, string yAxesName)
        {
            xAxesLabel.Content = xAxesName;
            yAxesLabel.Content = yAxesName;
        }
    }
}
