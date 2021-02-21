using EasyLanguageLearning.Domain.LanguageContents;
using Microsoft.EntityFrameworkCore;

using conv = EasyLanguageLearning.Infrastructure.DataModelConverters;

namespace EasyLanguageLearning.Infrastructure.LanguageContents
{
    public static class LanguageContentModelingExtensions
    {
        public static void BuildLanguageContentModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LanguageContent>(entity =>
                entity.HasKey(e => e.Id));
            var languageContentBuilder = modelBuilder.Entity<LanguageContent>();
            languageContentBuilder.Property(le => le.LessonId)
                .HasConversion(conv.LessonIdConverter)
                .HasColumnName(nameof(LanguageContent.LessonId)).IsRequired();
            languageContentBuilder.Property(le => le.MotherLanguageIso)
                .HasConversion(conv.IsoConverter);
            languageContentBuilder.Property(le => le.LearningLanguageIso)
                .HasConversion(conv.IsoConverter);
        }
    }
}
