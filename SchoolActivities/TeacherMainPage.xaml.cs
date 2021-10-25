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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolActivities
{
    /// <summary>
    /// Interaction logic for TeacherMainPage.xaml
    /// </summary>
    public partial class TeacherMainPage : Page
    {
        private Teacher teacher;

        public TeacherMainPage(Teacher teacher)
        {
            InitializeComponent();
            NamePrepod.DataContext = teacher;
            this.teacher = teacher;
            predmetComboBox.ItemsSource = this.teacher.Circles;
            GenerateCalendary();
        }

        void GenerateCalendary()
        {
            List<DayForCalendary> days = new List<DayForCalendary>();

            int nWeek = ((int)(new DateTime(DateTime.Today.Year, DateTime.Today.Month,1).DayOfWeek) + 6) %7;
            
            for (int i = 0; i < nWeek; i++)
            {
                days.Add(new DayForCalendary());
            }
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month); i++)
            {
                days.Add(new DayForCalendary(new DateTime(DateTime.Today.Year, DateTime.Today.Month, i)));
            }


            if (timeComboBox.SelectionBoxItem as string == "Месяц")
            {
                foreach (Circle circle in teacher.Circles)
                {
                    foreach (TimeTable time in circle.TimeTable)
                    {
                        foreach (DayForCalendary day in days)
                        {
                            if (day.Day != null)
                            {
                                if (day.Day.Value.Date == time.DateAndTime.Value.Date)
                                {
                                    day.Circles.Add(circle, day.Day);
                                }
                            }
                        }
                    }
                }
            }
            else if((timeComboBox.SelectedItem as string) == "Неделя")
            {
                foreach (Circle circle in teacher.Circles)
                {
                    foreach (TimeTable time in circle.TimeTable)
                    {
                        foreach (DayForCalendary day in days)
                        {
                            if (day.Day == time.DateAndTime && day.Day > DateTime.Today.AddDays(-7) && day.Day < DateTime.Today.AddDays(7))
                            {
                                day.Circles.Add(circle, day.Day);
                            }
                        }
                    }
                }
            }

            CircleCalendary.ItemsSource = days;
        } 

        private void SelectDayClick(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
