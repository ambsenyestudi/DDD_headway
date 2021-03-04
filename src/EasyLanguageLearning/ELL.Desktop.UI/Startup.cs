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
using static System.Net.Mime.MediaTypeNames;


//https://blog.hildenco.com/2020/05/configuration-in-net-core-console.html
namespace ELL.Desktop.UI
{
    public class Startup
    {
        
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //todo fix
            services.Configure<PathServiceSettings>(settings => Configuration.GetSection(nameof(PathServiceSettings)).Bind(settings));
            services.Configure<CourseSettings>(settings => Configuration.GetSection(nameof(CourseSettings)).Bind(settings));
            services.Configure<LessonSettings>(settings => Configuration.GetSection(nameof(LessonSettings)).Bind(settings));
            services.AddSingleton<IMessenger, Messenger>();
            services.AddHttpClient<IVocabularyService, VocabularyService>();
            services.AddHttpClient<LessonService>();
            services.AddScoped<ILessonService, LessonsCacheDecorator>(provider =>
                new LessonsCacheDecorator(
                    provider.GetService<LessonService>(),
                    provider.GetService<IMemoryCache>()));
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
        public void Configure(ApplicationBuilder application)
        {
            //Todo
        }
    }
}
