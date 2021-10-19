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
            //Проверка на данные у Учителя, если нету такого, то код внизу! Иначе переход на другое окно MainInfoWindow
            errorLogIn.Visibility = Visibility.Visible;
        }
    }
}
