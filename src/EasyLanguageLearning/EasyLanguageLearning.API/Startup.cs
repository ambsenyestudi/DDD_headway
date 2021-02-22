using EasyLanguageLearning.Application.LearningPaths;
using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Infrastructure;
using EasyLanguageLearning.Infrastructure.ContentSupplying;
using EasyLanguageLearning.Infrastructure.LearningPaths;
using EasyLanguageLearning.Infrastructure.VocabularyUnits;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EasyLanguageLearning.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<DataContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ILearningPathService, LearningPathService>();
            services.AddTransient<ILearningPathsRepository, LearningPathsRepository>();
            services.AddTransient<IVocabularyUnitRepository, VocabularyUnitRepository>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyLanguageLearning.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyLanguageLearning.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
