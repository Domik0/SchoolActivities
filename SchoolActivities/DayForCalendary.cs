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
        public List<CirclesForDay> Circles { get; set; }

        public DayForCalendary()
        {
            Day = null;
            Circles = new List<CirclesForDay>();
        }

        public DayForCalendary(DateTime Day)
        {
            Circles = new List<CirclesForDay>();
            this.Day = Day;
        }
    }

    public class CirclesForDay
    {
        public Circle cir { get; set; }
        public DateTime? dt { get; set; }
    }
}
