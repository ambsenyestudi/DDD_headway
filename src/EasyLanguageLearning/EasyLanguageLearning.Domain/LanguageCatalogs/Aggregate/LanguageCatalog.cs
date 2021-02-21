using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate
{
    public class LanguageCatalog
    {
        public LanguageCatalogId Id { get; protected set; }
        public Iso Iso { get; protected set; }
        public ICollection<LearningLanguage> Items { get; protected set; } = new List<LearningLanguage>();
        public LanguageCatalog()
        {
        }
        public LanguageCatalog(Guid id, Iso motherLanguageIso)
        {
            Id = new LanguageCatalogId(id);
            Iso = motherLanguageIso;
        }

        public void AddToCatalog(Iso iso, string LanguageName, Guid guid = new Guid())
        {
            if(guid == Guid.Empty)
            {
                guid = Guid.NewGuid();
            }
            var currLearningLanguage = new LearningLanguage(guid, iso, LanguageName, Id);
            Items.Add(currLearningLanguage);
        }

        
    }
}
