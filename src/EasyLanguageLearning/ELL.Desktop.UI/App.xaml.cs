using ELL.Desktop.UI.Extensions;
using ELL.Desktop.UI.Services;
using ELL.Desktop.UI.Services.Courses;
using ELL.Desktop.UI.Services.Lessons;
using ELL.Desktop.UI.Services.Paths;
using ELL.Desktop.UI.Services.VocabularyUnits;
using ELL.Desktop.UI.ViewModels;
using ELL.Desktop.UI.ViewModels.Base;
using ELL.Desktop.UI.Views;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace ELL.Desktop.UI
{
    //https://executecommands.com/dependency-injection-in-wpf-net-core-csharp/#:~:text=%20Implementing%20Dependency%20Injection%20in%20WPF%20application.%20,Employee.cs:%20This%20is%20the%20class%20that...%20More

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private ServiceProvider serviceProvider;
        public App()
        {
            serviceProvider = CreateHost().Build();
        }

        private ApplicationBuilder CreateHost() =>
            ApplicationBuilder.CreateDefaultBuilder();

        
        protected async override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
        


       
    }
}
