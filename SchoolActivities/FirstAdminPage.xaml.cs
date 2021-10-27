using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;

namespace SchoolActivities
{
    /// <summary>
    /// Interaction logic for FirstAdminPage.xaml
    /// </summary>
    public partial class FirstAdminPage : Page
    {
        public SeriesCollection SeriesViews { get; set; } = new SeriesCollection();

        public FirstAdminPage()
        {           
            InitializeComponent();
            Loaded += FirstAdminPage_Loaded;
            pieChart.DataContext = this;

            countStudentsInCirclesTextBlock.Text = App.db.Students.Count().ToString();
            countCirclesTextBlock.Text = App.db.Circles.Count().ToString();
            countTeacherTextBlock.Text = App.db.Teachers.Count().ToString();
            countCabinetsTextBlock.Text = App.db.Circles.Select(c => c.Cabinet).Count().ToString();
        }

        private void FirstAdminPage_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var circle in App.Circles)
            {
                SeriesViews.Add(new PieSeries
                {
                    Title = circle.Title,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(circle.Students.Count) },
                    DataLabels = true
                });
            }
        }
    }
}
