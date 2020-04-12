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

namespace Weather_forecast.Components.Table
{
    /// <summary>
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : Window
    {
        public ObservableCollection<LocationDailyWeather> LocationDaylyForecasts { get; set; }

        public TableView()
        {
            LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>();
            initializeRows();
            initializeComponents();
            InitializeComponent();
            setComponents();
        }

        private void initializeRows()
        {
            foreach (string key in MainWindow.forecast.locationForecast.Keys)
                addLocationDailyWeather(MainWindow.forecast.locationForecast[key]);
        }
       
        private void addLocationDailyWeather(LocationForecast lf) {
            foreach(LocationDailyWeather ldw in lf.ForecastDict.Values)
                LocationDaylyForecasts.Add(ldw);
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
        
        private void initializeComponents()
        {
            fromDate = new ComboBox();
            toDate = new ComboBox();
        }
        
        private void setComponents()
        {
            fromDate.Text = "Select date from";
            toDate.Text = "Select date to";

            fromDate.ItemsSource = MainWindow.forecast.getAllDates();
            toDate.ItemsSource = MainWindow.forecast.getAllDates();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Nullable<DateTime> from = fromDate.SelectedItem as Nullable<DateTime>;
            Nullable<DateTime> to = toDate.SelectedItem as Nullable<DateTime>;
            string cityName = filterCity.Text;

            tableView.ItemsSource = null;
            tableView.ItemsSource = applyFilter(from, to, cityName);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            LocationDaylyForecasts.Clear();
            initializeRows();
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
    }
}
