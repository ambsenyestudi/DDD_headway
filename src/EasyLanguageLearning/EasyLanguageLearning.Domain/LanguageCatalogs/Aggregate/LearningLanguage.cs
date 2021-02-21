using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;

namespace EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate
{
    public class LearningLanguage
    {
        
        public LearningLanguageId Id { get; }
        public LanguageCatalogId LanguageCatalogId { get; protected set; }
        public string Name { get; protected set; }
        public Iso Iso { get; }
        
        protected LearningLanguage()
        {

        }
        
        internal LearningLanguage(Guid id, Iso iso, string languageName, LanguageCatalogId catalogId)
        {
            Id = new LearningLanguageId(id);
            LanguageCatalogId = catalogId;
            Name = languageName;
            Iso = iso;
        }
                
    }
}
