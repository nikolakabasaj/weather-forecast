using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Weather_forecast.JSONModels.Current_forecast;
using Weather_forecast.Models;

namespace Weather_forecast.XAML_Components.Graph
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView : Window
    {
        public GraphView()
        {
            InitializeComponent();
           
        }

        private void loadLineChartData()
        {
            ((LineSeries)chartView.Series[0]).ItemsSource =
            new KeyValuePair<DateTime, int>[]{
            new KeyValuePair<DateTime,int>(DateTime.Now, 100),
            new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(1), 130),
            new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(2), 150),
            new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(3), 125),
            new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(4),155) };

            KeyValuePair<DateTime, int>[] valueList = new KeyValuePair<DateTime, int>[] {
            new KeyValuePair<DateTime,int>(DateTime.Now, 150),
            new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(1), 180),
            new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(2), 200),
            new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(3), 225),
            new KeyValuePair<DateTime,int>(DateTime.Now.AddMonths(4),255) };
        
            LineSeries lineSeries1 = new LineSeries();
            lineSeries1.Title = "Title";
            lineSeries1.DependentValuePath = "Value";
            lineSeries1.IndependentValuePath = "Key";
            lineSeries1.ItemsSource = valueList;
            chartView.Series.Add(lineSeries1);
        }

        public void setComponents(string graphData, string[] cities)
        {
            foreach (string city in cities)
            {
                var locationForecast = MainWindow.forecast.locationForecast[city];
                KeyValuePair<DateTime, double>[] valueList = locationForecast.ForecastDict
                                                        .Select(pair => new KeyValuePair<DateTime, double>(pair.Key, getAttributeValueByName(graphData, pair.Value)))
                                                        .ToArray();
                double a = getAttributeValueByName(graphData, locationForecast.ForecastDict.First().Value);
                LineSeries lineSeries = new LineSeries();
                lineSeries.Title = city.ToUpper();
                lineSeries.DependentValuePath = "Value";
                lineSeries.IndependentValuePath = "Key";
                lineSeries.ItemsSource = valueList;
                chartView.Series.Add(lineSeries);
            }
        }

        private double getAttributeValueByName(string attributeName, LocationDailyWeather ldw) {
            double retVal = 0;
            if (attributeName == "Celsius")
            {
                retVal =  ldw.Celsius;

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
    }
}
