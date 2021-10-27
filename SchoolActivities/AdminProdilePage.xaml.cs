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
    /// Interaction logic for AdminProdilePage.xaml
    /// </summary>
    public partial class AdminProdilePage : Page
    {
        Teacher teacher;
        public AdminProdilePage(Teacher teacher)
        {
            InitializeComponent();
            this.teacher = teacher;
            profileGrid.DataContext = teacher;
            fioTextBox.Text = teacher.LastName + " " + teacher.FirstName + " " + teacher.Patronymic;

            List<int> stagYears = new List<int>();
            for (int i = 0; i < 99; i++)
            {
                stagYears.Add(i);
            }
            stagComboBox.ItemsSource = stagYears;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (phoneTextBox.Text != "" && passwordTextBox.Text != "" && birthdayDatePicker.SelectedDate != null)
            {
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
                    teacher.Experience = stagComboBox.SelectedIndex;
                    teacher.Password = passwordTextBox.Text;

                    App.db.SaveChanges();

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
