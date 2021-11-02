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
    /// Interaction logic for AdminCirclesInfoPage.xaml
    /// </summary>
    public partial class AdminCirclesInfoPage : Page
    {
        Circle doCircke;
        AdminCirclesPage parent;
        bool isAdd;

        private List<string> listCabinet = new List<string>()
        {
            "101",
            "102",
            "103",
            "104",
            "105",
            "106",
            "107"
        };

        public AdminCirclesInfoPage(Circle circle, AdminCirclesPage parent, bool isAdd)
        {
            InitializeComponent();

            this.isAdd = isAdd;
            this.parent = parent;

            doCircke = circle;
            circleGrid.DataContext = circle;
            teachersComboBox.ItemsSource = App.db.Teachers.ToList();
            teachersComboBox.SelectedItem = circle.Teacher;
            kabinetComboBox.ItemsSource = listCabinet;
            kabinetComboBox.SelectedItem = circle.Cabinet;
            titleCircleTextBox.Text = circle.Title;
        }
        public AdminCirclesInfoPage(AdminCirclesPage parent, bool isAdd)
        {
            InitializeComponent();

            this.isAdd = isAdd;
            this.parent = parent;

            teachersComboBox.ItemsSource = App.db.Teachers.ToList();
            kabinetComboBox.ItemsSource = App.db.Circles.ToList().Distinct();
            deleteButton.Visibility = Visibility.Hidden;
            saveButton.Visibility = Visibility.Hidden;
            addButton.Visibility = Visibility.Visible;
            titleCircleTextBox.Text = "Название кружка";
            titleCircleTextBox.IsReadOnly = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (isAdd)
            {
                if (titleCircleTextBox.Text != "" || titleCircleTextBox.Text == "Название кружка")
                {
                    if (teachersComboBox.SelectedIndex != -1 && kabinetComboBox.SelectedIndex != -1)
                    {
                        Teacher teacher = teachersComboBox.SelectedItem as Teacher;
                        string dopCircle = kabinetComboBox.SelectedItem as string;

                        Circle circle = new Circle()
                        {
                            Id = App.db.Circles.ToList().Last().Id + 1,
                            Title = titleCircleTextBox.Text,
                            Teacher = teacher,
                            Cabinet = dopCircle
                        };
                        App.db.Circles.Add(circle);
                        App.db.SaveChanges();

                        parent.UpdateListCircles();
                        NavigationService.GoBack();
                    }
                    else
                    {
                        errorTextBlock.Visibility = Visibility.Visible;
                        errorTextBlock.Text = "Заполните все поля!";
                    }
                }
                else
                {
                    errorTextBlock.Visibility = Visibility.Visible;
                    errorTextBlock.Text = "Заполните все поля!";
                }
            }
            else
            {
                Teacher teacher = teachersComboBox.SelectedItem as Teacher;
                Circle circle = App.db.Circles.Where(i => i.Id == doCircke.Id).FirstOrDefault();

                circle.Title = titleCircleTextBox.Text;
                circle.Teacher.LastName = teacher.LastName;
                circle.Teacher.FirstName = teacher.FirstName;
                circle.Teacher.Patronymic = teacher.Patronymic;

                Circle dopCircle = kabinetComboBox.SelectedItem as Circle;
                circle.Cabinet = dopCircle.Cabinet;

                App.db.SaveChanges();

                parent.UpdateListCircles();
                NavigationService.GoBack();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Circle circle = App.db.Circles.Where(i => i.Id == doCircke.Id).FirstOrDefault();
            App.db.Circles.Remove(circle);
            App.db.SaveChanges();

            parent.UpdateListCircles();
            NavigationService.GoBack();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
