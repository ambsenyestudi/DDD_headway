using EasyLanguageLearning.Domain.Evaluations;
using EasyLanguageLearning.Domain.Evaluations.Aggregate;
using EasyLanguageLearning.Domain.VocabularyUnits;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyLanguageLearning.Infrastructure.Evaluations
{
    public static class EvaluationModelingExtensions
    {
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
            
            writingExerciseBuilder.Property(le => le.VocabularyId)
                .HasConversion(woI => woI.Value, guid => new VocabularyId(guid))
                .HasColumnName(nameof(WritingExercise.VocabularyId))
                .IsRequired();

            BuildWritingExerciseAnswerKey(writingExerciseBuilder);


        }
        public static void BuildWritingExerciseAnswerKey(EntityTypeBuilder<WritingExercise> writingExerciseBuilder)
        {
            writingExerciseBuilder.OwnsOne(we => we.AnswerKey)
                //this gives accessto EF to the protected property named answer
                .Property("answer");
        }
    }
}
