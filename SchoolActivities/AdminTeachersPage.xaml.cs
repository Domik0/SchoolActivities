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

        public AdminTeachersPage()
        {
            InitializeComponent();
            teacher.ItemsSource = teachers;
        }

        private void Search_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Search.Text.Length == 0)
            {
                teacher.ItemsSource = teachers;
            }
            else
            {
                teacher.ItemsSource = teachers.Where(c => c.LastName.ToString().StartsWith(Search.Text)).ToList();
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text.Length == 0)
            {
                teacher.ItemsSource = teachers;
            }
            else
            {
                //teacher.ItemsSource = teachers.Where(c => c.LastName.ToString().StartsWith(Search.Text)).ToList();
                teacher.ItemsSource = teachers.Where(c => c.LastName.ToLower().Contains(Search.Text.ToLower())).ToList();

            }
        }

        private void fire_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void change_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void save_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void addTeacher_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
