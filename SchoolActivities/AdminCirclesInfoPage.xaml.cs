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
        public AdminCirclesInfoPage(Circle circle, AdminCirclesPage parent)
        {
            InitializeComponent();
            doCircke = circle;
            this.parent = parent;
            circleGrid.DataContext = circle;
            teachersComboBox.ItemsSource = App.db.Teachers.ToList();
            teachersComboBox.SelectedItem = circle.Teacher;
            kabinetComboBox.ItemsSource = App.db.Circles.ToList();
            kabinetComboBox.SelectedItem = circle;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Teacher teacher = teachersComboBox.SelectedItem as Teacher;
            Circle circle = App.db.Circles.Where(i => i.Id == doCircke.Id).FirstOrDefault();

            circle.Teacher.LastName = teacher.LastName;
            circle.Teacher.FirstName = teacher.FirstName;
            circle.Teacher.Patronymic = teacher.Patronymic;

            Circle dopCircle = kabinetComboBox.SelectedItem as Circle;
            circle.Cabinet = dopCircle.Cabinet;

            //App.db.SaveChanges();

            parent.UpdateListCircles();
            NavigationService.GoBack();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Circle circle = App.db.Circles.Where(i => i.Id == doCircke.Id).FirstOrDefault();
            App.db.Circles.Remove(circle);
            //App.db.SaveChanges();

            parent.UpdateListCircles();
            NavigationService.GoBack();
        }
    }
}
