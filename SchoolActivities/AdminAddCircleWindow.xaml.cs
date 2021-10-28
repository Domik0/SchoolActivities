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

        public AdminAddCircleWindow(AdminRaspisaniePage page,  DateTime? dt)
        {
            this.page = page;
            dateTime = dt;
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
            Console.WriteLine();
            App.db.SaveChanges();
            page.GenerateCalendary();
            Close();
        }
    }
}
