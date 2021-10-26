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
using System.Data.Entity;

namespace SchoolActivities
{
    /// <summary>
    /// Interaction logic for AdminCirclesPage.xaml
    /// </summary>
    public partial class AdminCirclesPage : Page
    {
        bool isAdd;
        public AdminCirclesPage()
        {
            InitializeComponent();
            UpdateListCircles();
        }

        private void CirclesList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Circle circle = circlesList.SelectedItem as Circle;
            if (circle != null)
            {
                isAdd = false;
                AdminMainPage.frame.Content = new AdminCirclesInfoPage(circle, this, isAdd);
            }
        }

        public void UpdateListCircles()
        {
            circlesList.ItemsSource = App.db.Circles.Include(r => r.Teacher).ToList();
        }

        private void plusCircleImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isAdd = true;
            AdminMainPage.frame.Content = new AdminCirclesInfoPage(this, isAdd);
        }
    }
}
