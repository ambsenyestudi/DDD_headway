using EasyLanguageLearning.Application.Courses;
using EasyLanguageLearning.Infrastructure.ContentSupplying;
using Microsoft.EntityFrameworkCore;

namespace EasyLanguageLearning.Infrastructure
{
    public class DataContext : DbContext
    {
        //https://github.com/ardalis/CleanArchitecture/blob/master/src/Clean.Architecture.Web/SeedData.cs
        //https://github.com/charlesbill/TheSolutionArchitect/blob/dev-branch/test-mvc-webapp/Data/MvcWebAppDbContext.cs
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

        }
        public DbSet<CourseDTO> Courses { get; set; }
        public DbSet<LearningPathDM> LearningPaths { get; set; }

    }
}
