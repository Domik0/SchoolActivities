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
    /// Interaction logic for AdminTeachersPage.xaml
    /// </summary>
    public partial class AdminTeachersPage : Page
    {
        List<Teacher> teachers = App.db.Teachers.ToList();
        bool isAdd;
        public AdminTeachersPage()
        {
            InitializeComponent();
            teacherList.ItemsSource = teachers;
        }

        private void Search_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Search.Text.Length == 0)
            {
                teacherList.ItemsSource = teachers;
            }
            else
            {
                teacherList.ItemsSource = teachers.Where(c => c.LastName.ToString().StartsWith(Search.Text)).ToList();
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text.Length == 0)
            {
                teacherList.ItemsSource = teachers;
            }
            else
            {
                //teacher.ItemsSource = teachers.Where(c => c.LastName.ToString().StartsWith(Search.Text)).ToList();
                teacherList.ItemsSource = teachers.Where(c => c.LastName.ToLower().Contains(Search.Text.ToLower())).ToList();

            }
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            isAdd = true;
            AdminMainPage.frame.Content = new AdminAllProfilePage(this, isAdd);
        }

        private void DeleteButtom_Click(object sender, RoutedEventArgs e)
        {
            var item = ((Button)sender).DataContext as Teacher;
            App.db.Teachers.Remove(item);
            App.db.SaveChanges();

            UpdateListTeachers();
        }

        private void UpdateButtom_Click(object sender, RoutedEventArgs e)
        {
            isAdd = false;
            var item = ((Button)sender).DataContext as Teacher;
            AdminMainPage.frame.Content = new AdminAllProfilePage(this, item, isAdd);
        }
        public void UpdateListTeachers()
        {
            teacherList.ItemsSource = null;
            teacherList.ItemsSource = App.db.Teachers.ToList();
        }
    }
}
