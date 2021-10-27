using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
            timeComboBox.ItemsSource = new List<string>()
            {
                "Месяц",
                "Неделя"
            };
            List<Circle> circles = new List<Circle>();
            circles.Add(new Circle()
            {
                Title = "Все кружки"
            });
            circles.AddRange(this.teacher.Circles);
            predmetComboBox.ItemsSource = circles;
            GenerateCalendary();
        }

        void GenerateCalendary()
        {
            List<DayForCalendary> days = new List<DayForCalendary>();

            int nWeek = ((int)(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).DayOfWeek) + 6) % 7;

            for (int i = 0; i < nWeek; i++)
            {
                days.Add(new DayForCalendary());
            }
            for (int i = 1; i <= DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month); i++)
            {
                days.Add(new DayForCalendary(new DateTime(DateTime.Today.Year, DateTime.Today.Month, i)));
            }



            foreach (Circle circle in teacher.Circles)
            {
                if (circle == predmetComboBox.SelectedItem as Circle || (predmetComboBox.SelectedItem as Circle)?.Title == "Все кружки")
                {
                    foreach (TimeTable time in circle.TimeTable)
                    {
                        foreach (DayForCalendary day in days)
                        {
                            if (timeComboBox.SelectedItem as string == "Месяц")
                            {
                                if (day.Day != null)
                                {
                                    if (day.Day.Value.Date == time.DateAndTime.Value.Date)
                                    {
                                        day.Circles.Add(new CirclesForDay() { cir = circle, dt = time.DateAndTime });
                                    }
                                }
                            }
                            else if ((timeComboBox.SelectedItem as string) == "Неделя")
                            {
                                if (day.Day != null)
                                {
                                    if (day.Day.Value.Date == time.DateAndTime.Value.Date && day.Day > DateTime.Today.AddDays(-7) && day.Day < DateTime.Today.AddDays(7))
                                    {
                                        day.Circles.Add(new CirclesForDay() { cir = circle, dt = day.Day });
                                    }
                                    if (day.Day < DateTime.Today.AddDays(-7) || day.Day > DateTime.Today.AddDays(7))
                                    {
                                        day.Day = null;
                                    }
                                }  
                            }
                        }
                    }
                }
            }

            Calendary.ItemsSource = days;
        }

        private void CircleInDay_click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(
                new TeacherCircleDayPage(((sender as Border).DataContext) as CirclesForDay));
        }

        private void ComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Calendary != null)
            {
                Calendary.ItemsSource = null;
                GenerateCalendary();
            }
        }
    }
}
