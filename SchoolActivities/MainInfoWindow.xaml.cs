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
using System.Windows.Shapes;

namespace SchoolActivities
{
    /// <summary>
    /// Interaction logic for MainInfoWindow.xaml
    /// </summary>
    public partial class MainInfoWindow : Window
    {
        public MainInfoWindow(Teacher teacher)
        {
            InitializeComponent();
            Application.Current.MainWindow.Close();
            if (teacher.AdministratorStatus == true)
            {
                frame.Content = new AdminMainPage(teacher);
            }
            else
            {
                frame.Content = new TeacherMainPage(teacher);
            }
        }
    }
}
