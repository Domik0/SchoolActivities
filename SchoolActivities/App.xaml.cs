using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SchoolActivities
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SchoolActivitiesEntities db = new SchoolActivitiesEntities();

        public static List<Circle> Circles { get; private set; }
        public static List<Student> Students { get; private set; }

        public App()
        {
            Startup += App_Startup;
        }

        private async void App_Startup(object sender, StartupEventArgs e)
        {
            await LoadedCollectionsDbAsync();
        }

        private async Task LoadedCollectionsDbAsync()
        {
            await Task.Run(async () =>
            {
                Circles = await db.Circles.ToListAsync();
                Students = await db.Students.ToListAsync();

            });
        }
    }
}
