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
        bool isAdd;
        AdminStudentsPage parent;
        Student dopStudent;
        List<CirclesInStudent> circles = new List<CirclesInStudent>();
        List<Circle> isusCircles = new List<Circle>();
        public AdminAllProfilePage(Student student, AdminStudentsPage adminStudentsPage, bool isAdd)
        {
            InitializeComponent();
            this.isAdd = isAdd;
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


            foreach (var stud in App.db.Circles.ToList())
            {
                circles.Add(new CirclesInStudent()
                {
                    Name = stud.Title,
                    Selected = false
                });
            }
            foreach (var item in circles)
            {
                foreach (var stud in student.Circles)
                {
                    if (item.Name == stud.Title)
                    {
                        item.Selected = true;
                    }
                }
            }
            circleComboBox.ItemsSource = circles;
        }
        public AdminAllProfilePage(AdminStudentsPage adminStudentsPage, bool isAdd)
        {
            InitializeComponent();

            this.isAdd = isAdd;
            parent = adminStudentsPage;

            addButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Hidden;

            List<string> classes = new List<string>();
            for (int i = 1; i < 12; i++)
            {
                classes.Add(i.ToString());
            }
            classComboBox.ItemsSource = classes;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (isAdd)
            {
                if (fioTextBox.Text != "" && birthdayDatePicker.SelectedDate != null && classComboBox.SelectedItem != null)
                {
                    Student student = new Student();
                    string[] fioMas = fioTextBox.Text.Split(' ');

                    if (fioMas.Length < 3)
                    {
                        errorLogIn.Text = "Введите ФИО полностью. С пробелами!";
                        errorLogIn.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        student.Id = App.db.Students.ToList().Last().Id + 1;
                        student.LastName = fioMas[0];
                        student.FirstName = fioMas[1];
                        student.Patronymic = fioMas[2];
                        student.Birthday = birthdayDatePicker.SelectedDate;
                        student.ClassGroup = classComboBox.SelectedItem.ToString();

                        App.db.Students.Add(student);
                        App.db.SaveChanges();

                        parent.UpdateListCircles();
                        NavigationService.GoBack();
                    }
                }
                else
                {
                    errorLogIn.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (fioTextBox.Text != "" && birthdayDatePicker.SelectedDate != null && classComboBox.SelectedItem != null)
                {
                    Student student = App.db.Students.Where(s => s.Id == dopStudent.Id).FirstOrDefault();
                    string[] fioMas = fioTextBox.Text.Split(' ');

                    if (fioMas.Length < 3)
                    {
                        errorLogIn.Text = "Введите ФИО полностью. С пробелами!";
                        errorLogIn.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        student.LastName = fioMas[0];
                        student.FirstName = fioMas[1];
                        student.Patronymic = fioMas[2];
                        student.Birthday = birthdayDatePicker.SelectedDate;
                        student.ClassGroup = classComboBox.SelectedItem.ToString();
                        //student.Circles = circleComboBox.SelectedItems;

                        App.db.SaveChanges();

                        parent.UpdateListCircles();
                        NavigationService.GoBack();
                    }
                }
                else
                {
                    errorLogIn.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
