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
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace SchoolActivities
{
    /// <summary>
    /// Interaction logic for AdminStudentsPage.xaml
    /// </summary>
    public partial class AdminStudentsPage : Page
    {
        bool isAdd;
        ObservableCollection<Circle> circles = new ObservableCollection<Circle>();
        List<Student> students1 = new List<Student>();
        public AdminStudentsPage()
        {
            InitializeComponent();
            UpdateListCircles();
            PPS();

        }

        private void AddStudentImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isAdd = true;
            AdminMainPage.frame.Content = new AdminAllProfilePage(this, isAdd);
        }

        private void CirclesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateListCircles();
        }

        private void UpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            isAdd = false;
            Student student = studentsInCirclesListView.SelectedItem as Student;
            AdminMainPage.frame.Content = new AdminAllProfilePage(student, this, isAdd);
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            Student student = studentsInCirclesListView.SelectedItem as Student;
            App.db.Students.Remove(student);
            App.db.SaveChanges();

            UpdateListCircles();
        }
        public void UpdateListCircles()
        {
            students1 = App.db.Students.ToList();
            var students = circlesComboBox.SelectedItem as Circle;

            studentsInCirclesListView.ItemsSource = null;
            studentsInCirclesListView.ItemsSource = students.Students.ToList();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var student = (circlesComboBox.SelectedItem as Circle).Students.ToList();

            if (Search.Text.Length == 0)
            {
                studentsInCirclesListView.ItemsSource = student;
            }
            else
            {
                studentsInCirclesListView.ItemsSource = student.Where(c => c.LastName.ToLower().Contains(Search.Text.ToLower()) 
                                                                           || c.FirstName.ToLower().Contains(Search.Text.ToLower())
                                                                           || c.Patronymic.ToLower().Contains(Search.Text.ToLower())).ToList();

            }
        }
        public void PPS()
        {
            circles.Clear();
            circles.Add(new Circle()
            {
                Title = "Все кружки",
                Students = students1
            });
            foreach (var item in App.db.Circles.Include(r => r.Students).ToList())
            {
                circles.Add(item);
            }
            circlesComboBox.ItemsSource = circles;
        }
    }
}
