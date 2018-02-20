using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinIoTGrovePi.Controller;
using WinIoTGrovePi.Model;


using GrovePi;
using GrovePi.Sensors;
using GrovePi.I2CDevices;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WinIoTGrovePi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private LiveChartsController lcc;
        private Random rng = new Random();

        DispatcherTimer dt;

        IDHTTemperatureAndHumiditySensor sensor = DeviceFactory.Build.DHTTemperatureAndHumiditySensor(Pin.DigitalPin4, DHTModel.Dht11);

        public MainPage()
        {
            this.InitializeComponent();

            this.dt = new DispatcherTimer();
            this.dt.Interval = TimeSpan.FromSeconds(10);
            this.dt.Tick += Dt_Tick;
            this.dt.Start();

            lcc = new LiveChartsController();

            this.DataContext = lcc;


        }

        private void Dt_Tick(object sender, object e)
        {
            sensor.Measure();
            double sensortemp = sensor.TemperatureInCelsius;

            lcc.addToChart(new DateModel(sensortemp, DateTime.Now));
        }

        private async void bt_Test_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
