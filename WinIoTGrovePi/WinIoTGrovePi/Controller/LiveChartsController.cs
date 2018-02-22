using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Uwp;
using TempSensorChart.Model;
using TempSensorChart.Tools;

namespace TempSensorChart.Controller
{
    public class LiveChartsController : NotifyPropertyChangedBase
    {
        public SeriesCollection SeriesCollection { get; set; }
        public LineSeries LineSeries { get; set; }
        public Func<double, string> Formatter { get; set; }

        public LiveChartsController()
        {
            var dayConfig = Mappers.Xy<DateModel>()
                .X(dateModel => dateModel.DateTime.Ticks / TimeSpan.FromSeconds(10).Ticks)
                .Y(dateModel => dateModel.Value);

            SeriesCollection = new SeriesCollection(dayConfig);

            OnPropertyChanged("SeriesCollection");

            Formatter = value => new DateTime((long)(value * TimeSpan.FromSeconds(10).Ticks)).ToString("t");

            LineSeries = new LineSeries { Title = "", Values = new ChartValues<DateModel> { } };

            SeriesCollection.Add(LineSeries);
        }

        public void addToChart(DateModel dm)
        {
            if (SeriesCollection[0].Values.Count >= 5)
            {
                SeriesCollection[0].Values.RemoveAt(0);
                SeriesCollection[0].Values.Add(dm);
            }
            else
            {
                SeriesCollection[0].Values.Add(dm);
            }
            OnPropertyChanged("SeriesCollection");
        }
    }
}
