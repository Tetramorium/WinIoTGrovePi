using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using GrovePi.Sensors;
using GrovePi;
using System.Threading.Tasks;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace LedButtonPotentiometer
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // 
            // TODO: Insert code to perform background work
            //
            // If you start any asynchronous methods here, prevent the task
            // from closing prematurely by using BackgroundTaskDeferral as
            // described in http://aka.ms/backgroundtaskdeferral
            //

            // Build Sensors

            IButtonSensor Button = DeviceFactory.Build.ButtonSensor(Pin.DigitalPin6);

            ILed Red = DeviceFactory.Build.Led(Pin.DigitalPin2);
            ILed Blue = DeviceFactory.Build.Led(Pin.DigitalPin3);
            ILed Green = DeviceFactory.Build.Led(Pin.DigitalPin4);

            IRotaryAngleSensor Potentiometer = DeviceFactory.Build.RotaryAngleSensor(Pin.AnalogPin0);

            // Set initial values

            int State = 0;
            double Speed = 100;

            while (true)
            {
                Speed = Potentiometer.SensorValue();

                // Speed can be adjusted between 0-1023
                // Adjust values for a range between 100-1000

                if (Speed < 100)
                    Speed = 100;

                if (Speed > 1000)
                    Speed = 1000;

                //Get button state

                string buttonon = Button.CurrentState.ToString();
                bool buttonison = buttonon.Equals("On", StringComparison.OrdinalIgnoreCase);

                if (buttonison)
                {
                    // Turn off all Leds and then turn on current Led

                    Red.AnalogWrite(Convert.ToByte(0));
                    Green.AnalogWrite(Convert.ToByte(0));
                    Blue.AnalogWrite(Convert.ToByte(0));

                    switch (State)
                    {
                        case 0:
                            Red.AnalogWrite(Convert.ToByte(255));
                            break;
                        case 1:
                            Blue.AnalogWrite(Convert.ToByte(255));
                            break;
                        case 2:
                            Green.AnalogWrite(Convert.ToByte(255));
                            break;
                    }

                    // If State is above 2 reset loop else add 1

                    if (State == 2)
                        State = 0;
                    else
                        State++;
                }

                // Delay the task according to the Potentiometer value

                Task.Delay((int)Speed).Wait();
            }
        }
    }
}
