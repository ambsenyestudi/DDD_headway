using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace ELL.Desktop.UI.Extensions
{
    public class ApplicationBuilder
    {
        public ServiceCollection serviceCollection = new ServiceCollection();
        public static ApplicationBuilder CreateDefaultBuilder() =>
            new ApplicationBuilder();

        public ServiceProvider Build()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables(); 
            var startUp = new Startup(builder.Build());
            startUp.ConfigureServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

    }
}
