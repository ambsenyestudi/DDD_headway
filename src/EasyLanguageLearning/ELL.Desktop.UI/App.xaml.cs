using ELL.Desktop.UI.Services;
using ELL.Desktop.UI.Services.Courses;
using ELL.Desktop.UI.Services.Lessons;
using ELL.Desktop.UI.Services.Paths;
using ELL.Desktop.UI.ViewModels;
using ELL.Desktop.UI.ViewModels.Base;
using ELL.Desktop.UI.Views;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
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
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
            
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IMessenger, Messenger>();
            services.AddHttpClient<ILessonService, LessonService>();
            services.AddHttpClient<CourseService>();
            services.AddScoped<ICourseService, CourseCacheDecorator>(provider =>
                new CourseCacheDecorator(
                    provider.GetService<CourseService>(),
                    provider.GetService<IMemoryCache>()));
            services.AddHttpClient<PathService>();
            services.AddScoped<IPathService, PathCacheDecorator>(provider => 
                new PathCacheDecorator(
                    provider.GetService<PathService>(),
                    provider.GetService<IMemoryCache>()));
            services.AddSingleton<ITextService, TextService>();
            services.AddSingleton<LoadingViewModel>();
            services.AddSingleton<INavigationPage, LoadingPage>();
            services.AddSingleton<CoursesViewModel>();
            services.AddSingleton<INavigationPage, CoursePage>();
            services.AddSingleton<WelcomeViewModel>();
            services.AddSingleton<INavigationPage, WelcomePage>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddMemoryCache();

        }
        
        protected async override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
        


       
    }
}
