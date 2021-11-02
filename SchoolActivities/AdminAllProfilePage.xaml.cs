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
        AdminStudentsPage parentStudent;
        Student dopStudent;
        List<int> stagYears = new List<int>();
        List<string> classes = new List<string>();
        List<Circle> circles = App.db.Circles.ToList();
        List<Circle> trueCircles = new List<Circle>();

        bool isTeacher = false;
        AdminTeachersPage parentTeacher;
        Teacher dopTeacher;
        public AdminAllProfilePage(Student student, AdminStudentsPage adminStudentsPage, bool isAdd)
        {
            InitializeComponent();
            this.isAdd = isAdd;
            parentStudent = adminStudentsPage;

            dopStudent = student;

            gridData.DataContext = student;
            fioTextBox.Text = student.LastName + " " + student.FirstName + " " + student.Patronymic;

            birthdayDatePicker.SelectedDate = student.Birthday;

            for (int i = 1; i < 12; i++)
            {
                classes.Add(i.ToString());
            }
            classComboBox.ItemsSource = classes;
            classComboBox.SelectedItem = student.ClassGroup;

            foreach (var item in circles)
            {
                foreach (var stud in student.Circles)
                {
                    if (item.Title == stud.Title)
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
            parentStudent = adminStudentsPage;

            addButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Hidden;

            List<string> classes = new List<string>();
            for (int i = 1; i < 12; i++)
            {
                classes.Add(i.ToString());
            }
            classComboBox.ItemsSource = classes;
            circleComboBox.ItemsSource = circles;
        }

        public AdminAllProfilePage(AdminTeachersPage parent, Teacher teacher, bool isAdd)
        {
            InitializeComponent();
            isTeacher = true;
            parentTeacher = parent;
            dopTeacher = teacher;
            this.isAdd = isAdd;

            gridData.DataContext = teacher;
            comboStackPanel.Visibility = Visibility.Hidden;
            classTextBlock.Text = "Лет";

            List<int> stagYears = new List<int>();
            for (int i = 0; i < 99; i++)
            {
                stagYears.Add(i);
            }
            classComboBox.ItemsSource = stagYears;

            phoneStackPanel.Visibility = Visibility.Visible;
            statusStackPanel.Visibility = Visibility.Visible;
            passwordStackPanel.Visibility = Visibility.Visible;

            fioTextBox.Text = teacher.LastName + " " + teacher.FirstName + " " + teacher.Patronymic;
            birthdayDatePicker.SelectedDate = teacher.Birthday;
            classComboBox.SelectedIndex = Convert.ToInt32(teacher.Experience);
            phoneTextBox.Text = teacher.PhoneNumber;
            if (teacher.AdministratorStatus == true)
            {
                statusCheckBox.IsChecked = true;
            }
            else
            {
                statusCheckBox.IsChecked = false;
            }
            passwordTextBox.Text = teacher.Password;


        }
        public AdminAllProfilePage(AdminTeachersPage parent, bool isAdd)
        {
            InitializeComponent();
            isTeacher = true;
            this.isAdd = isAdd;
            parentTeacher = parent;

            comboStackPanel.Visibility = Visibility.Hidden;
            classTextBlock.Text = "Лет";

            for (int i = 0; i < 99; i++)
            {
                stagYears.Add(i);
            }
            classComboBox.ItemsSource = stagYears;

            phoneStackPanel.Visibility = Visibility.Visible;
            statusStackPanel.Visibility = Visibility.Visible;
            passwordStackPanel.Visibility = Visibility.Visible;

            classComboBox.SelectedIndex = 0;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (isTeacher)
            {
                if (isAdd)
                {
                    if (birthdayDatePicker != null && phoneTextBox.Text != "" && passwordTextBox.Text != "")
                    {
                        Teacher teacher = new Teacher();
                        string[] fioMas = fioTextBox.Text.Split(' ');

                        if (fioMas.Length < 3)
                        {
                            errorLogIn.Text = "Введите ФИО полностью. С пробелами!";
                            errorLogIn.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            teacher.Id = App.db.Teachers.ToList().Last().Id + 1;
                            teacher.FirstName = fioMas[1];
                            teacher.LastName = fioMas[0];
                            teacher.Patronymic = fioMas[2];
                            teacher.Birthday = birthdayDatePicker.SelectedDate;
                            teacher.PhoneNumber = phoneTextBox.Text;
                            teacher.Experience = classComboBox.SelectedIndex;
                            teacher.AdministratorStatus = statusCheckBox.IsChecked;
                            teacher.Password = passwordTextBox.Text;

                            App.db.Teachers.Add(teacher);
                            App.db.SaveChanges();

                            parentTeacher.UpdateListTeachers();
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
                    if (birthdayDatePicker.SelectedDate != null && phoneTextBox.Text != "" && passwordTextBox.Text != "")
                    {
                        Teacher teacher = App.db.Teachers.Where(t => t.Id == dopTeacher.Id).FirstOrDefault();
                        string[] fioMas = fioTextBox.Text.Split(' ');

                        if (fioMas.Length < 3)
                        {
                            errorLogIn.Text = "Введите ФИО полностью. С пробелами!";
                            errorLogIn.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            teacher.FirstName = fioMas[1];
                            teacher.LastName = fioMas[0];
                            teacher.Patronymic = fioMas[2];
                            teacher.Birthday = birthdayDatePicker.SelectedDate;
                            teacher.PhoneNumber = phoneTextBox.Text;
                            teacher.Experience = classComboBox.SelectedIndex;
                            teacher.AdministratorStatus = statusCheckBox.IsChecked;
                            teacher.Password = passwordTextBox.Text;

                            App.db.SaveChanges();

                            parentTeacher.UpdateListTeachers();
                            NavigationService.GoBack();
                        }
                    }
                    else
                    {
                        errorLogIn.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                if (isAdd)
                {
                    if (fioTextBox.Text != "" && birthdayDatePicker.SelectedDate != null && classComboBox.SelectedItem != null && circleComboBox.SelectedItem != null)
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

                            foreach (var item in circles)
                            {
                                if (item.Selected == true)
                                {
                                    trueCircles.Add(item);
                                }
                            }
                            student.Circles = trueCircles;

                            App.db.Students.Add(student);
                            App.db.SaveChanges();

                            parentStudent.UpdateListCircles();
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

                            foreach (var item in circles)
                            {
                                if (item.Selected == true)
                                {
                                    trueCircles.Add(item);
                                }
                            }
                            student.Circles = trueCircles;

                            App.db.SaveChanges();

                            parentStudent.UpdateListCircles();
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

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void birthdayDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isTeacher)
            {
                DatePicker datePicker = (sender as DatePicker);
                int countYear = 1;
                if (datePicker.SelectedDate.HasValue && (DateTime.Now <= birthdayDatePicker.SelectedDate.Value.AddYears(20) ||
                    (DateTime.Now.Year - birthdayDatePicker.SelectedDate.Value.Year) > 65))
                {
                    birthdayDatePicker.SelectedDate = null;
                }
                else if (datePicker.SelectedDate.HasValue)
                {
                    countYear = DateTime.Now.Year - birthdayDatePicker.SelectedDate.Value.AddYears(20).Year;
                    stagYears = new List<int>();
                    for (int i = 1; i < countYear; i++)
                    {
                        stagYears.Add(i);
                    }
                    classComboBox.ItemsSource = stagYears;
                }
            }
            else
            {
                DatePicker datePicker = (sender as DatePicker);
                int countYear = 1;
                if (datePicker.SelectedDate.HasValue && (DateTime.Now <= birthdayDatePicker.SelectedDate.Value.AddYears(7) ||
                    (DateTime.Now.Year - birthdayDatePicker.SelectedDate.Value.AddYears(7).Year) > 12))
                {
                    birthdayDatePicker.SelectedDate = null;
                }
                else if (datePicker.SelectedDate.HasValue)
                {
                    countYear = DateTime.Now.Year - birthdayDatePicker.SelectedDate.Value.Year - 7;
                    classes = new List<string>();
                    for (int i = countYear - 2; i < countYear + 2; i++)
                    {
                        if (i < 12 && i > 0)
                        {
                            classes.Add(i.ToString());
                        }
                    }
                    classComboBox.ItemsSource = classes;
                }
            }
        }
    }
}
