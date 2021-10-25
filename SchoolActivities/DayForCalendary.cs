using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolActivities
{
    public class DayForCalendary
    {
        public DateTime? Day { get; set; }
        public Dictionary<Circle, DateTime?> Circles { get; set; }

        public DayForCalendary()
        {
            Day = null;
            Circles = new Dictionary<Circle, DateTime?>();
        }

        public DayForCalendary(DateTime Day)
        {
            Circles = new Dictionary<Circle, DateTime?>();
            this.Day = Day;
        }
    }
}
