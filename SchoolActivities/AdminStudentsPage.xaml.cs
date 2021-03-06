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
using System.Collections.ObjectModel;

namespace SchoolActivities
{
    /// <summary>
    /// Interaction logic for AdminStudentsPage.xaml
    /// </summary>
    public partial class AdminStudentsPage : Page
    {
        bool isAdd;
        List<Circle> circles = App.db.Circles.Include(r => r.Students).ToList();

        public AdminStudentsPage()
        {
            InitializeComponent();
            circlesComboBox.ItemsSource = circles;
        }

        private void AddStudentImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isAdd = true;
            AdminMainPage.frame.Content = new AdminAllProfilePage(this, isAdd);
        }
        private void CirclesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListCircles();
        }
        private void UpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            isAdd = false;
            Student student = studentsInCirclesListView.SelectedItem as Student;
            AdminMainPage.frame.Content = new AdminAllProfilePage(student, this, isAdd);
        }
        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            Student student = studentsInCirclesListView.SelectedItem as Student;
            App.db.Students.Remove(student);
            App.db.SaveChanges();

            UpdateListCircles();
        }
        public void UpdateListCircles()
        {
            if (circlesComboBox.SelectedItem != null)
            {
                var student = circlesComboBox.SelectedItem as Circle;

                studentsInCirclesListView.ItemsSource = null;
                studentsInCirclesListView.ItemsSource = student.Students.OrderBy(s => s.LastName).ToList();
            }
            else
            {
                studentsInCirclesListView.ItemsSource = null;
                studentsInCirclesListView.ItemsSource = App.db.Students.OrderBy(s => s.LastName).ToList();
                circlesComboBox.SelectedItem = null;
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var student = new List<Student>();
            var selectedItem = (circlesComboBox.SelectedItem as Circle);

            if(selectedItem != null)
            {
                student = selectedItem.Students.ToList();
            }
            else
            {
                student = App.db.Students.ToList();
            }
            if (Search.Text.Length == 0)
            {
                studentsInCirclesListView.ItemsSource = student;
            }
            else
            {
                studentsInCirclesListView.ItemsSource = student.Where(c => c.LastName.ToLower().Contains(Search.Text.ToLower())
                                                                           || c.FirstName.ToLower().Contains(Search.Text.ToLower())
                                                                           || c.Patronymic.ToLower().Contains(Search.Text.ToLower())
                                                                           || c.ClassGroup.ToLower().Contains(Search.Text.ToLower())).ToList();
            }
        }

        private void ShowAllStudent(object sender, MouseButtonEventArgs e)
        {
            studentsInCirclesListView.ItemsSource = null;
            studentsInCirclesListView.ItemsSource = App.db.Students.ToList();
            circlesComboBox.SelectedItem = null;
        }
    }
}
