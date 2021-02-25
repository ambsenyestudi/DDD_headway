using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            BuildVocabularyItemModel(modelBuilder);
            languageContentBuilder.HasMany<Vocabulary>(vu => vu.VocabularyItems)
                .WithOne()
                .HasForeignKey(v => v.VocabularyUnitId);
                
        }
        
        private static void BuildVocabularyItemModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vocabulary>().HasKey(vo => vo.Id);
            var vocabularyBuilder = modelBuilder.Entity<Vocabulary>();
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
        //this cases a problem
        public static void BuildWritingExerciseModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WritingExercise>(entity =>
                entity.HasKey(e => e.Id));
            var writingExerciseBuilder = modelBuilder.Entity<WritingExercise>();
            writingExerciseBuilder.Property(we => we.Id)
                .HasConversion(weId => weId.Value,
                guid => new WritingExerciseId(guid))
            .IsRequired();
            writingExerciseBuilder.Property(le => le.Id)
                .HasConversion(wex => wex.Value, guid => new WritingExerciseId(guid))
                .HasColumnName(nameof(WritingExercise.Id))
                .IsRequired();
            //todo
            writingExerciseBuilder.Property(le => le.VocabularyId)
                .HasConversion(woI => woI.Value, guid => new VocabularyId(guid))
                .HasColumnName(nameof(WritingExercise.VocabularyId))
                .IsRequired();

            writingExerciseBuilder.OwnsOne(we => we.AnswerKey);
        }
    }
}
