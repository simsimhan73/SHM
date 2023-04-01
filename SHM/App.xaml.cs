using Microsoft.Extensions.DependencyInjection;
using SHM.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SHM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient(typeof(MainWindowViewModel));
            services.AddTransient(typeof(StartUpViewModel));
            services.AddTransient(typeof(SeatViewModel));
            

            return services.BuildServiceProvider();
        }
    }
}
