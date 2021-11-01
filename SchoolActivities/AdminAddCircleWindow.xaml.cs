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

        public AdminAddCircleWindow(AdminRaspisaniePage page, DayForCalendary day)
        {
            this.day = day;
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
            App.db.TimeTables.Add(new TimeTable()
            {
                Id = App.db.TimeTables.ToList().Last().Id + 1,
                Circle = predmetComboBox.SelectedItem as Circle,
                DateAndTime = dateTime
            });
            bool flagFreeTeacher = true;
            bool flagFreeCabinet = true;
            foreach (var circlesForDay in day.Circles.Where(c => c.cir.Teacher == (predmetComboBox.SelectedItem as Circle).Teacher).ToList())
            {
                if (circlesForDay.dt < dateTime.Value.AddHours(2) || circlesForDay.dt > dateTime.Value.AddHours(-2))
                {
                    flagFreeTeacher = false;
                }
            }
            foreach (var circlesForDay in day.Circles.Where(c => c.cir.Cabinet == (predmetComboBox.SelectedItem as Circle).Cabinet).ToList())
            {
                if (circlesForDay.dt < dateTime.Value.AddHours(2) || circlesForDay.dt > dateTime.Value.AddHours(-2))
                {
                    if(circlesForDay.cir.Cabinet == (predmetComboBox.SelectedItem as Circle).Cabinet)
                    {
                        flagFreeCabinet = false;
                    }
                }
            }
            if(flagFreeTeacher && flagFreeCabinet)
            {
                App.db.SaveChanges();
                page.GenerateCalendary();
                Close();
            }
            else if (!flagFreeTeacher && !flagFreeCabinet)
            {
                MessageBox.Show("В это время учитель и кабинет заняты", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!flagFreeTeacher && flagFreeCabinet)
            {
                MessageBox.Show("В это время учитель занят", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (flagFreeTeacher && !flagFreeCabinet)
            {
                MessageBox.Show("В это время кабинет занят", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
