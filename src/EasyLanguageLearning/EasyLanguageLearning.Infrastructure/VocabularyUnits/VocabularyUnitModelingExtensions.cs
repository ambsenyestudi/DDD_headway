using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using Microsoft.EntityFrameworkCore;
using conv = EasyLanguageLearning.Infrastructure.DataModelConverters;

namespace EasyLanguageLearning.Infrastructure.VocabularyUnits
{
    public static class VocabularyUnitModelingExtensions
    {
        public static void BuildVocabularyUnitModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VocabularyUnit>(entity =>
                entity.HasKey(e => e.Id));
            var languageContentBuilder = modelBuilder.Entity<VocabularyUnit>();
            languageContentBuilder.Property(vu => vu.Id)
                .HasConversion(vuId => vuId.Value, guid => new VocabularyUnitId(guid))
                .HasColumnName(nameof(VocabularyUnit.Id))
                .IsRequired();
            languageContentBuilder.Property(le => le.LessonId)
                .HasConversion(le => le.Value, guid => new LessonId(guid))
                .HasColumnName(nameof(VocabularyUnit.LessonId))
                .IsRequired();
            languageContentBuilder.Property(le => le.MotherLanguageIso)
                .HasConversion(conv.IsoConverter);
            languageContentBuilder.Property(le => le.LearningLanguageIso)
                .HasConversion(conv.IsoConverter);
            //todo map collection property
            /*
            languageContentBuilder.HasMany<Vocabulary>(vu => vu.VocabularyItems)
                .WithOne(vo=>vo.VocabularyUnit)
                .HasForeignKey(vo=>vo.VocabularyUnitId);
            */
            BuildVocabularyItemModel(modelBuilder);


        }
        
        private static void BuildVocabularyItemModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vocabulary>().HasKey(vo => vo.Id);
            //one to many relations
            modelBuilder.Entity<Vocabulary>().HasOne<VocabularyUnit>().WithMany().HasForeignKey(e => e.VocabularyUnitId);
           


            var vocabularyBuilder = modelBuilder.Entity<Vocabulary>();
            //vocabularyBuilder.HasOne<VocabularyUnit>(x => x.VocabularyUnit).WithMany(vu=>vu.VocabularyItems).HasForeignKey(vo => vo.VocabularyUnitId);
            //todo map list relation
            vocabularyBuilder.Property(vo => vo.Id)
                .HasConversion(
                    woI => woI.Value, 
                    guid => new VocabularyId(guid));
            vocabularyBuilder.Property(vo => vo.VocabularyUnitId)
                .HasConversion(
                vuId => vuId.Value, 
                guid => new VocabularyUnitId(guid))
                .IsRequired();
            
        }
        
    }
}
