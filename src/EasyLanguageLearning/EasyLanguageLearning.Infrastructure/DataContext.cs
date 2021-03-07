using EasyLanguageLearning.Domain.Evaluations.Aggregate;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using EasyLanguageLearning.Infrastructure.Evaluations;
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
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<WritingExercise> WritingExercises { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildLearningPathModel();
            modelBuilder.BuildLangaugeCatalogModel();
            modelBuilder.BuildVocabularyUnitModel();
            modelBuilder.BuildWritingExerciseModel();

            modelBuilder.SeedLanguageCatalogs();

            /*
            modelBuilder.SeedLeaningPaths(this);
            var unit = SeedVocabulary.Create();
            modelBuilder.Entity<Vocabulary>().HasData(unit.VocabularyItems);
            modelBuilder.Entity<VocabularyUnit>().HasData(unit);
            */
            base.OnModelCreating(modelBuilder);

        }
    }
}
