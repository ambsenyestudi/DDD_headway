using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.VocabularyUnits
{
    public class SeedVocabulary
    {
        public const string EN_FR_FIRST_UNIT = "3322627b-bfa6-498d-914a-720f873b08a8";
        public const string EN_FR_FIRST_LESSON = "e667a2bd-2f01-43f9-b796-2c322690c6f7";


        public static VocabularyUnit Create()
        {
            var vocUnit = TranslateEnFr(
                En_FR_Translations,
                vocUnitId: new Guid(EN_FR_FIRST_UNIT));
            return vocUnit;
        }
        private static VocabularyUnit TranslateEnFr(Dictionary<string, string> transltions,
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
            var lessonId = new LessonId(new Guid(EN_FR_FIRST_LESSON));//SeedLearningPath.enFR["LessonId"]));
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

        private static Dictionary<string, string> En_FR_Translations { get; } =
            new Dictionary<string, string>
            {
                ["Yes"] = "Oui",
                ["No"] = "Non",
                ["Good bye"] = "Au revoir"
            };
    }
}
