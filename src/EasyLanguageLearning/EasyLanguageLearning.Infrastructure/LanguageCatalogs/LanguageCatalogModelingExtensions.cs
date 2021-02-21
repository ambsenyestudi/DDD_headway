using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using conv = EasyLanguageLearning.Infrastructure.DataModelConverters;

namespace EasyLanguageLearning.Infrastructure.LanguageCatalogs
{
    public static class LanguageCatalogModelingExtensions
    {
        public static void BuildLangaugeCatalogModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LanguageCatalog>(entity =>
                entity.HasKey(e => e.Id));
            var languageCatalogBuilder = modelBuilder.Entity<LanguageCatalog>();
            languageCatalogBuilder.ToTable(nameof(LanguageCatalogs));
            languageCatalogBuilder.Property(lc => lc.Id)
                .HasConversion(lcId => lcId.Value, guid => new LanguageCatalogId(guid))
                .IsRequired();
            languageCatalogBuilder.Property(lc => lc.Iso)
                .HasConversion(conv.IsoConverter);

            languageCatalogBuilder.OwnsMany(
                    lc => lc.Items,
                    ll => ll.BuildLearningLanguageModel()
                );
        }

        private static void BuildLearningLanguageModel(this OwnedNavigationBuilder<LanguageCatalog, LearningLanguage> leaningLangaugeModel)
        {
            leaningLangaugeModel.Property(ll => ll.LanguageCatalogId)
                .HasConversion(llId => llId.Value, lpGuid => new LanguageCatalogId(lpGuid))
                .HasColumnName(nameof(LearningLanguage.LanguageCatalogId))
                .IsRequired();
            leaningLangaugeModel.Property(ll => ll.Id)
                .HasConversion(conv.LearningLanguageIdConverter)
                .HasColumnName(nameof(LearningLanguage.Id))
                .IsRequired();
            leaningLangaugeModel.Property(lc => lc.Iso)
                .HasConversion(conv.IsoConverter);

            leaningLangaugeModel.HasKey(co => co.Id);

            leaningLangaugeModel.WithOwner().HasConstraintName(nameof(LearningLanguage.LanguageCatalogId));
        }

    }
}
