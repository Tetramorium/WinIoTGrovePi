using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinIoTGrovePi.Model
{
    public class DateModel
    {
        public DateTime DateTime { get; set; }
        public double Value { get; set; }

        public DateModel(double _Value, DateTime _DateTime)
        {
            this.Value = _Value;
            this.DateTime = _DateTime;
        }
    }
}
