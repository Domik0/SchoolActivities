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
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        public FirstPage()
        {
            InitializeComponent();
        }

        private void UserPhoneNumberText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userPhoneNumberText.Text != "")
            {
                prevUserPhoneNumberText.Opacity = 0;
            }
            else
            {
                prevUserPhoneNumberText.Opacity = 0.6;
            }
        }

        private void userPasswordText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (userPasswordText.Text != "")
            {
                prevUserPasswordText.Opacity = 0;
            }
            else
            {
                prevUserPasswordText.Opacity = 0.6;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на данные у Учителя, если нету такого, то проверка на Админа, иначе код внизу! И переход на другое окно MainInfoWindow
            var teacher = App.db.Teachers.Where(t => t.PhoneNumber == userPhoneNumberText.Text).FirstOrDefault();
            if (teacher != null && teacher.Password == userPasswordText.Text)
            {
                if (teacher.AdministratorStatus == true)
                {
                    //открывает страничку админа
                    NavigationService.Navigate(new AdminMainPage(teacher));
                }
                else
                {
                    //открывает странчику учителя
                    NavigationService.Navigate(new MainInfoWindow(teacher));
                }
            }
            else
            {
                //выдает ошибочку
                errorLogIn.Visibility = Visibility.Visible;
            }
        }
    }
}
