using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using EasyLanguageLearning.Infrastructure;
using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.API.Seeding
{
    public static class SeedVocabulary
    {
        public const string EN_FR_FIRST_UNIT = "3322627b-bfa6-498d-914a-720f873b08a8";
        public static void Populate(DataContext dbContext)
        {
            
            foreach (var item in dbContext.VocabularyUnits)
            {
                dbContext.Remove(item);
            }
            var vocUnit = TranslateEnFr(
                En_FR_Translations,
                vocUnitId: new Guid(EN_FR_FIRST_UNIT));
            dbContext.VocabularyUnits.Add(vocUnit);
            dbContext.SaveChanges();
        }

        private static VocabularyUnit TranslateEnFr(Dictionary<string, string> transltions, 
            IsoCodes motherIsoCode = IsoCodes.en, 
            IsoCodes learningIsoCode = IsoCodes.fr, 
            Guid vocUnitId = new Guid())
        {
            if(vocUnitId == Guid.Empty)
            {
                vocUnitId = Guid.NewGuid();
            }
            var motherIso = Iso.CreateIso(motherIsoCode);
            var learningIso = Iso.CreateIso(learningIsoCode);
            var vocUnit =  new VocabularyUnit(vocUnitId,
                motherIso,
                learningIso);
            //Todo add translations
            foreach (var vocabularyItem in transltions)
            {
                var term = TranslatedContent.Create(
                    motherIso,
                    vocabularyItem.Key,
                    learningIso,
                    vocabularyItem.Value
                    );
                vocUnit.AddVocabulary(term);
            }
            return vocUnit;
        }

        private static Dictionary<string, string> En_FR_Translations { get; } =
            new Dictionary<string, string>
            {
                ["Yes"] = "Oui",
                ["No"] = "Non",
                ["Good bye"] = "Au revoir"
            };
    }
}
