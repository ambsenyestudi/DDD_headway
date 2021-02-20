using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Infrastructure.ContentSupplying;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace EasyLanguageLearning.Infrastructure
{
    public class DataContext : DbContext
    {
        public const string DB_SCHEMA = "test";
        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<LearningPathDM> LearningPathsDB { get; set; }
        //https://github.com/ardalis/CleanArchitecture/blob/master/src/Clean.Architecture.Web/SeedData.cs
        //https://github.com/charlesbill/TheSolutionArchitect/blob/dev-branch/test-mvc-webapp/Data/MvcWebAppDbContext.cs
        //https://kontext.tech/column/dotnet_framework/275/sqlite-in-net-core-with-entity-framework-core
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // Map table names
            BuildDomainModel(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }
        private void BuildDMModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LearningPathDM>().ToTable(nameof(LearningPathsDB));
            modelBuilder.Entity<LearningPathDM>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
        private void BuildDomainModel(ModelBuilder modelBuilder)
        {
            var learningPathBuilder = modelBuilder.Entity<LearningPath>();
            learningPathBuilder.ToTable(nameof(LearningPaths));
            learningPathBuilder.Property(x => x.Id).HasConversion(x => x.Value, l => new LearningPathId(l)).HasColumnName("Id").IsRequired();
            
            modelBuilder.Entity<LearningPath>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
