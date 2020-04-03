using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Weather_forecast.Utility
{
    public class Clock
    {
        public DispatcherTimer timer = new DispatcherTimer();
        public int tickIncrement = 0;

        public Clock(EventHandler clockTicker)
        {
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += clockTicker;
            timer.Start();
        }

        public string[] getCurrentTime()
        {
            int hours = DateTime.Now.Hour;
            int minutes = DateTime.Now.Minute;
            int seconds = DateTime.Now.Second;

            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
            }
            if (minutes == 60)
            {
                minutes = 0;
                hours++;
            }

            return validateAll(seconds, minutes, hours);
        }
       
        private string[] validateAll(int seconds, int minutes, int hours)
        {
            string[] values = { validate(hours), validate(minutes), validate(seconds) };
            return values;
        }
        private string validate(int num)
        {
            if(num < 10)
            {
                return "0" + num;
            }
            return num.ToString(); 
        }
    }
}
