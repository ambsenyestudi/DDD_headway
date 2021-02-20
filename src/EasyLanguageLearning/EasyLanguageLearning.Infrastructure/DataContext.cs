using EasyLanguageLearning.Infrastructure.ContentSupplying;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace EasyLanguageLearning.Infrastructure
{
    public class DataContext : DbContext
    {
        public const string DB_SCHEMA = "test";
        public DbSet<LearningPathDM> LearningPaths { get; set; }
        //https://github.com/ardalis/CleanArchitecture/blob/master/src/Clean.Architecture.Web/SeedData.cs
        //https://github.com/charlesbill/TheSolutionArchitect/blob/dev-branch/test-mvc-webapp/Data/MvcWebAppDbContext.cs
        //https://kontext.tech/column/dotnet_framework/275/sqlite-in-net-core-with-entity-framework-core
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            var todo = "";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // Map table names
            modelBuilder.Entity<LearningPathDM>().ToTable(nameof(LearningPaths), DB_SCHEMA);
            modelBuilder.Entity<LearningPathDM>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            base.OnModelCreating(modelBuilder);

        }

    }
}
