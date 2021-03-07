using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.SeedingBackgroundProcess.Stores
{
    public class VocabularyStore
    {
        public const string EN_FR_FIRST_UNIT = "3322627b-bfa6-498d-914a-720f873b08a8";
        

        public VocabularyUnit CreateEnFr(Dictionary<string, string> transltions,
            IsoCodes motherIsoCode = IsoCodes.en,
            IsoCodes learningIsoCode = IsoCodes.fr,
            Guid vocUnitId = new Guid())
        {
            if (vocUnitId == Guid.Empty)
            {
                vocUnitId = Guid.NewGuid();
            }
            var motherIso = Iso.CreateIso(motherIsoCode);
            var learningIso = Iso.CreateIso(learningIsoCode);
            var lessonId = new LessonId(new Guid(LearningPathStore.enFR["LessonId"]));
            var vocUnit = new VocabularyUnit(vocUnitId,
                lessonId,
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
        public Dictionary<string, string> GetEnFrLesson1() =>
            En_FR_Translations;
        private Dictionary<string, string> En_FR_Translations { get; } =
            new Dictionary<string, string>
            {
                ["Yes"] = "Oui",
                ["No"] = "Non",
                ["Good bye"] = "Au revoir"
            };
    }
}
