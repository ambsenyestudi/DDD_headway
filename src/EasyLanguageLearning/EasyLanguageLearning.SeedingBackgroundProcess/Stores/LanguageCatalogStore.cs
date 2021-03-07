using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;

namespace EasyLanguageLearning.SeedingBackgroundProcess.Stores
{
    public class LanguageCatalogStore
    {
        public const string LANGUAGE_CATALOG_ID = "188008a3-d7e9-48c5-890f-478d6d04e6a9";

        public const string EN_FRENCH_ID = "5e91a9f4-344f-4889-8ba3-2e2195bdc9c5";
        public const string EN_SPANISH_ID = "85b36f27-76f8-4530-a941-72cdc7971ebd";
        public const string EN_GERMAN_ID = "3ea46f15-be8c-448d-b751-9563390ce9bc";

        public const string EN_FRENCH = "French";
        public const string EN_SPANISH = "Spanish";
        public const string EN_GERMAN = "German";
      
        public LanguageCatalog Create()
        {
            var catalog = new LanguageCatalog(new Guid(LANGUAGE_CATALOG_ID), Iso.CreateIso(IsoCodes.en));

            catalog.AddToCatalog(Iso.CreateIso(IsoCodes.fr), EN_FRENCH, new Guid(EN_FRENCH_ID));
            catalog.AddToCatalog(Iso.CreateIso(IsoCodes.es), EN_SPANISH, new Guid(EN_SPANISH_ID));
            catalog.AddToCatalog(Iso.CreateIso(IsoCodes.de), EN_GERMAN, new Guid(EN_GERMAN_ID));
            return catalog;
        }
    }
}
