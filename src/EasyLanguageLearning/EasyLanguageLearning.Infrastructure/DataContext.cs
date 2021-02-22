using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using EasyLanguageLearning.Infrastructure.LanguageCatalogs;
using EasyLanguageLearning.Infrastructure.LearningPaths;
using EasyLanguageLearning.Infrastructure.VocabularyUnits;
using Microsoft.EntityFrameworkCore;

namespace EasyLanguageLearning.Infrastructure
{
    public class DataContext : DbContext
    {

        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<LanguageCatalog> LanguageCatalogs { get; set; }
        public DbSet<VocabularyUnit> VocabularyUnits { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.BuildLearningPathModel();
            modelBuilder.BuildLangaugeCatalogModel();
            modelBuilder.BuildVocabularyUnitModel();
            base.OnModelCreating(modelBuilder);

        }
    }
}
