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

namespace Weather_forecast.Components.Table
{
    /// <summary>
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : Window
    {
        private static Forecast forecast;
        public ObservableCollection<LocationDailyWeather> LocationDaylyForecasts { get; set; }

        public TableView()
        {
            forecast = MainWindow.forecast;
            LocationDaylyForecasts = new ObservableCollection<LocationDailyWeather>();
            addRows();
            //this.DataContext = LocationDaylyForecasts;
            InitializeComponent();
        }

        private void addRows()
        {
            foreach (string key in MainWindow.forecast.forecast.Keys)
                addLocationDailyWeather(MainWindow.forecast.forecast[key]);
        }
       
        private void addLocationDailyWeather(LocationForecast lf) {
            foreach(LocationDailyWeather ldw in lf.ForecastDict.Values)
                LocationDaylyForecasts.Add(ldw);
        }
    }
}
