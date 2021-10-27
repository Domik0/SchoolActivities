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
    /// Interaction logic for AdminAllProfilePage.xaml
    /// </summary>
    public partial class AdminAllProfilePage : Page
    {
        public AdminAllProfilePage(Student student)
        {
            InitializeComponent();
            gridData.DataContext = student;
            fioTextBox.Text = student.LastName + " " + student.FirstName + " " + student.Patronymic;
        }
    }
}
