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
            //Подсказка по номеру телефона
            if (userPhoneNumberText.Text != "")
            {
                prevUserPhoneNumberText.Opacity = 0;
            }
            else
            {
                prevUserPhoneNumberText.Opacity = 0.6;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на данные у Учителя, если нету такого, то проверка на Админа, иначе код внизу! И переход на другое окно MainInfoWindow
            var teacher = App.db.Teachers.Where(t => t.PhoneNumber == userPhoneNumberText.Text).FirstOrDefault();
            if (teacher != null && teacher.Password == userPasswordText.Password)
            {
                //открывает главное окно с инфой
                MainInfoWindow mw = new MainInfoWindow(teacher);
                mw.ShowDialog();
            }
            else
            {
                //выдает ошибочку если некорректные данные
                errorLogIn.Visibility = Visibility.Visible;
            }
        }

        private void userPasswordText_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Подсказка по паролю
            if (userPasswordText.Password != "")
            {
                prevUserPasswordText.Opacity = 0;
            }
            else
            {
                prevUserPasswordText.Opacity = 0.6;
            }
        }
    }
}
