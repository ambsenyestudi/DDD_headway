using EasyLanguageLearning.Application.LanguageCatalog;
using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.LanguageCatalogs
{
    public class LanguageCatalogService : ILanguageCatalogService
    {
        private readonly ILanguageCatalogRepository repository;

        public LanguageCatalogService(ILanguageCatalogRepository repository)
        {
            this.repository = repository;
        }
        public async Task<Guid> InsertLanguage(LearningLanguageDTO learningLanguage)
        {
            var catalog = await GetCatalog(learningLanguage);
            if (catalog == null || 
                !Iso.TryParse(learningLanguage.Iso, out Iso iso) ||
                PresentInCatalog(catalog, learningLanguage))
            {
                return Guid.Empty;
            }
            
            catalog.AddToCatalog(iso, learningLanguage.Name, learningLanguage.Id);
            var resultingId = await repository.Upsert(catalog);
            return resultingId.Value;
        }
        private async Task<LanguageCatalog> GetCatalog(LearningLanguageDTO learningLanguage)
        {
            var catalog = await repository.GetBy(new LanguageCatalogId(learningLanguage.LanguageCatalogId));
            if (catalog == null && Iso.TryParse(learningLanguage.MotherIso, out Iso currIso))
            {
                catalog = new LanguageCatalog(learningLanguage.LanguageCatalogId, currIso);
            }
            return catalog;
        }
        

        private bool PresentInCatalog(LanguageCatalog catalog, LearningLanguageDTO learningLanguage) =>
            PresentInCatalog(catalog, new LearningLanguageId(learningLanguage.Id));

        private bool PresentInCatalog(LanguageCatalog catalog, LearningLanguageId learningLanguageId) =>
            catalog.Items.Any(ll => ll.Id == learningLanguageId);
    }
}
