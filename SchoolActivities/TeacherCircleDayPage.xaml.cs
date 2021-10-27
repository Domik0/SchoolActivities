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
    /// Interaction logic for TeacherCircleDayPage.xaml
    /// </summary>
    public partial class TeacherCircleDayPage : Page
    {
        private Circle circle;
        private TimeTable timeTable;
        private List<Student> reportList;

        public TeacherCircleDayPage(CirclesForDay cfd)
        {
            this.circle = cfd.cir;
            this.timeTable = circle.TimeTable.First(t => t.DateAndTime == cfd.dt);
            reportList = timeTable.Students.ToList();
            InitializeComponent();
            BorderInfoCircle.DataContext = circle;
            CircleTimeInfo.DataContext = timeTable;
            studentsInCircleListView.ItemsSource = circle.Students;
        }

        private void GoBack_Click(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void StudentInLesson(object sender, MouseButtonEventArgs e)
        {
            if (((sender as Border).Child as Image).Visibility == Visibility.Hidden)
            {
                ((sender as Border).Child as Image).Visibility = Visibility.Visible;
                reportList.Add((sender as Border).DataContext as Student);
            }
            else
            {
                ((sender as Border).Child as Image).Visibility = Visibility.Hidden;
                reportList.Remove((sender as Border).DataContext as Student);
            }
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            timeTable.Students = reportList;
            App.db.SaveChanges();
        }

        private void FrameworkElement_OnInitialized(object sender, EventArgs e)
        {
            var student = (sender as Border).DataContext as Student;
            var imageFlag = (sender as Border).Child as Image;
            if (timeTable.Students.Count(t => t == student) > 0)
            {
                imageFlag.Visibility = Visibility.Visible;
            }
        }
    }
}
