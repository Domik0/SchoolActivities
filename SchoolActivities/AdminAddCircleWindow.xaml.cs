using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for AdminAddCircleWindow.xaml
    /// </summary>
    public partial class AdminAddCircleWindow : Window
    {
        private DateTime? dateTime = null;
        private AdminRaspisaniePage page;
        private DayForCalendary day;
        private List<DayForCalendary> month;

        public AdminAddCircleWindow(AdminRaspisaniePage page, DayForCalendary day, List<DayForCalendary> month)
        {
            this.day = day;
            this.month = month;
            this.page = page;
            dateTime = day.Day;
            InitializeComponent();
            DateTitle.DataContext = dateTime;
            timeTitle.DataContext = dateTime;
            predmetComboBox.ItemsSource = App.db.Circles.ToList();
        }

        private void SaveCircle(object sender, MouseButtonEventArgs e)
        {
            DateTime time = DateTime.ParseExact(timeTitle.Text, "HH:mm",
                CultureInfo.InvariantCulture);
            dateTime = new DateTime(dateTime.Value.Year, dateTime.Value.Month, dateTime.Value.Day, time.Hour, time.Minute, time.Second);
            int count = 0;
            if (AllWeek.IsChecked == true)
            {
                //count = 3 - new GregorianCalendar().GetWeekOfYear((DateTime)dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) % 4;
                while (dateTime.Value.AddDays(7 * count).Month == dateTime.Value.Month)
                {
                    count++;
                }
                count--;
            }
            else
            {
                count = 0;
            }
            for (int i = 0; i <= count; i++)
            {
                bool flagFreeTeacher = true;
                bool flagFreeCabinet = true;
                DateTime dtCheck = dateTime.Value.AddDays(7 * i);
                DayForCalendary dayThis = (month.Count(m => m.Day.Value.Day == dtCheck.Day) > 0
                    ? month.First(m => m.Day.Value.Day == dtCheck.Day)
                    : new DayForCalendary());
                foreach (var circlesForDay in dayThis.Circles.Where(c => c.cir.Teacher == (predmetComboBox.SelectedItem as Circle).Teacher).ToList())
                {
                    if (circlesForDay.dt < dtCheck.AddHours(2) || circlesForDay.dt > dtCheck.AddHours(-2))
                    {
                        flagFreeTeacher = false;
                    }
                }
                foreach (var circlesForDay in dayThis.Circles.Where(c => c.cir.Cabinet == (predmetComboBox.SelectedItem as Circle).Cabinet).ToList())
                {
                    if (circlesForDay.dt < dtCheck.AddHours(2) || circlesForDay.dt > dtCheck.AddHours(-2))
                    {
                        if (circlesForDay.cir.Cabinet == (predmetComboBox.SelectedItem as Circle).Cabinet)
                        {
                            flagFreeCabinet = false;
                        }
                    }
                }
                if (flagFreeTeacher && flagFreeCabinet)
                {
                    App.db.TimeTables.Add(new TimeTable()
                    {
                        Id = App.db.TimeTables.Local.ToList().Last().Id + 1,
                        Circle = predmetComboBox.SelectedItem as Circle,
                        DateAndTime = dtCheck
                    });
                    App.db.SaveChanges();
                }
                else if (!flagFreeTeacher && !flagFreeCabinet)
                {
                    MessageBox.Show($"В {dateTime.Value.AddDays(7 * count)} учитель и кабинет №{(predmetComboBox.SelectedItem as Circle).Cabinet} заняты", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!flagFreeTeacher && flagFreeCabinet)
                {
                    MessageBox.Show($"В {dateTime.Value.AddDays(7 * count)} учитель занят", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (flagFreeTeacher && !flagFreeCabinet)
                {
                    MessageBox.Show($"В это время кабинет №{(predmetComboBox.SelectedItem as Circle).Cabinet} занят", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            page.GenerateCalendary();
            Close();
        }
    }
}
