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
using System.Data.Entity;

namespace SchoolActivities
{
    /// <summary>
    /// Interaction logic for AdminStudentsPage.xaml
    /// </summary>
    public partial class AdminStudentsPage : Page
    {
        List<Circle> circles = App.db.Circles.Include(r => r.Students).ToList();
        List<Student> students = App.db.Students.ToList();
        public AdminStudentsPage()
        {
            InitializeComponent();
            
            foreach (var item in circles)
            {
                circlesComboBox.Items.Add(item);
            }
        }

        private void AddStudentImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void CirclesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var student = circlesComboBox.SelectedItem as Circle;

            studentsInCirclesListView.ItemsSource = null;
            studentsInCirclesListView.ItemsSource = student.Students.ToList();
        }

        private void UpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            Student student = studentsInCirclesListView.SelectedItem as Student;

        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            Student student = studentsInCirclesListView.SelectedItem as Student;
            App.db.Students.Remove(student);
            App.db.SaveChanges();
        }
    }
}
