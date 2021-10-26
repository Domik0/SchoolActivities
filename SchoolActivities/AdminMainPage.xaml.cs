﻿using System;
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
    /// Interaction logic for AdminMainPage.xaml
    /// </summary>
    public partial class AdminMainPage : Page
    {
        public static Frame frame;
        public AdminMainPage(Teacher teacher)
        {
            InitializeComponent();
            frame = frameNavigation;
        }

        private void TeacherButton_Click(object sender, RoutedEventArgs e)
        {
            //Открывает страничку со всеми Учителями
            frameNavigation.Content = new TeachersProfilePage();
        }

        private void AdminTextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Открывает заного первую страницу по умолчанию
            frameNavigation.Content = new FirstAdminPage();
        }

        private void StatistikButton_Click(object sender, RoutedEventArgs e)
        {
            frameNavigation.Content = new AdminStatistikPage();
        }

        private void CirclesButton_Click(object sender, RoutedEventArgs e)
        {
            frameNavigation.Content = new AdminCirclesPage();
        }

        private void StudentsButton_Click(object sender, RoutedEventArgs e)
        {
            frameNavigation.Content = new AdminStudentsPage();
        }
    }
}
