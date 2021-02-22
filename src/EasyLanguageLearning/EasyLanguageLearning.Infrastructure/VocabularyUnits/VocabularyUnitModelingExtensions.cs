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
            languageContentBuilder.Property(le => le.Id)
                .HasConversion(vuId => vuId.Value, guid => new VocabularyUnitId(guid))
                .HasColumnName(nameof(VocabularyUnit.Id))
                .IsRequired();
            languageContentBuilder.Property(le => le.MotherLanguageIso)
                .HasConversion(conv.IsoConverter);
            languageContentBuilder.Property(le => le.LearningLanguageIso)
                .HasConversion(conv.IsoConverter);
            languageContentBuilder.OwnsMany(
                vu => vu.VocabularyItems,
                voc => voc.BuildVocabularyItemModel());
        }
        private static void BuildVocabularyItemModel(this OwnedNavigationBuilder<VocabularyUnit, Vocabulary> vocabularyBuilder)
        {
            vocabularyBuilder.HasKey(vo => vo.Id);
            vocabularyBuilder.WithOwner().HasConstraintName(nameof(Vocabulary.VocabularyUnitId));
            vocabularyBuilder.Property(vo => vo.VocabularyUnitId)
                .HasConversion(
                vuId => vuId.Value, 
                guid => new VocabularyUnitId(guid))
                .IsRequired();
        }
    }
}
