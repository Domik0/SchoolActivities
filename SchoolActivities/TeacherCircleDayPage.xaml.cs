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
        private List<Student> reportList = new List<Student>();

        public TeacherCircleDayPage(CirclesForDay cfd)
        {
            this.circle = cfd.cir;
            this.timeTable = circle.TimeTable.First(t => t.DateAndTime == cfd.dt);
            InitializeComponent();
            BorderInfoCircle.DataContext = circle;
            CircleTimeInfo.DataContext = timeTable;
            studentsInCircleListView.ItemsSource = timeTable.Students;
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

            }
        }
    }
}
