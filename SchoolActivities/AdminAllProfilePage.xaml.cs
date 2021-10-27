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
    /// Interaction logic for AdminAllProfilePage.xaml
    /// </summary>
    public partial class AdminAllProfilePage : Page
    {
        AdminStudentsPage parent;
        Student dopStudent;
        public AdminAllProfilePage(Student student, AdminStudentsPage adminStudentsPage)
        {
            InitializeComponent();
            parent = adminStudentsPage;

            dopStudent = student;

            gridData.DataContext = student;
            fioTextBox.Text = student.LastName + " " + student.FirstName + " " + student.Patronymic;

            birthdayDatePicker.SelectedDate = student.Birthday;

            List<string> classes = new List<string>();
            for (int i = 1; i < 12; i++)
            {
                classes.Add(i.ToString());
            }
            classComboBox.ItemsSource = classes;
            classComboBox.SelectedItem = student.ClassGroup;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (fioTextBox.Text != "" && birthdayDatePicker.SelectedDate != null && classComboBox.SelectedItem != null)
            {
                Student student = App.db.Students.Where(s => s.Id == dopStudent.Id).FirstOrDefault();
                string[] fioMas = fioTextBox.Text.Split(' ');

                student.LastName = fioMas[0];
                student.FirstName = fioMas[1];
                student.Patronymic = fioMas[2];
                student.Birthday = birthdayDatePicker.SelectedDate;
                student.ClassGroup = classComboBox.SelectedItem.ToString();

                App.db.SaveChanges();

                parent.UpdateListCircles();
                NavigationService.GoBack();
            }
        }
    }
}
