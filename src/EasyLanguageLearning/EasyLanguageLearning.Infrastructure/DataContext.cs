using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LanguageContents;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Infrastructure.LanguageCatalogs;
using EasyLanguageLearning.Infrastructure.LanguageContents;
using EasyLanguageLearning.Infrastructure.LearningPaths;
using Microsoft.EntityFrameworkCore;

namespace EasyLanguageLearning.Infrastructure
{
    public class DataContext : DbContext
    {

        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<LanguageCatalog> LanguageCatalogs { get; set; }
        public DbSet<LanguageContent> LanguageContents { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.BuildLearningPathModel();
            modelBuilder.BuildLangaugeCatalogModel();
            modelBuilder.BuildLanguageContentModel();
            base.OnModelCreating(modelBuilder);

        }
    }
}
