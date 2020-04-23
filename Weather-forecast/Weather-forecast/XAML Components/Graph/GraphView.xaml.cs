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
