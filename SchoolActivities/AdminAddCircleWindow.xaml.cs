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
using System.Windows.Shapes;

namespace SchoolActivities
{
    /// <summary>
    /// Interaction logic for AdminAddCircleWindow.xaml
    /// </summary>
    public partial class AdminAddCircleWindow : Window
    {
        private DateTime? dateTime = null;

        public AdminAddCircleWindow(DateTime? dt)
        {
            dateTime = dt;
            InitializeComponent();
            DateTitle.DataContext = dateTime;
            timeTitle.DataContext = dateTime;
            predmetComboBox.ItemsSource = App.db.Circles.ToList();
        }
    }
}
