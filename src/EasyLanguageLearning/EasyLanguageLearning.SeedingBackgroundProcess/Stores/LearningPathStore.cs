using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.SeedingBackgroundProcess.Stores
{
    public class LearningPathStore
    {
        public const string FIRST_LESSON_NAME = "Hello world!";
        #region EN_FR
        public static readonly Dictionary<string, string> enFR = new Dictionary<string, string>
        {
            ["PathId"] = "82d83571-5fdd-40c0-ac46-0eea57a19ab0",
            ["CourseId"] = "90b94554-5e8e-4406-9e4b-c1b90fd4cb2c",
            ["LessonId"] = "e667a2bd-2f01-43f9-b796-2c322690c6f7"
        };
        //"7ed72ffb-0c56-414c-ac59-70846f003949"
        //"e6c5d53d-edd9-4b05-9efe-7225eaaf5ab6"
        //"535c4bab-cfc3-4acb-89d5-96872ce68b11"
        #endregion EN_FR
        #region EN_ES
        public static readonly Dictionary<string, string> enES = new Dictionary<string, string>
        {
            ["PathId"] = "6f7567bd-55d0-4986-98f0-05ffb8fdc7d2",
            ["CourseId"] = "634aa2e5-6ceb-4f57-82d1-988db75f6c95",
            ["LessonId"] = "a6bc084e-1fdd-4609-b41d-38332f0ccf5c"
        };
        //"681ba771-4bb0-463e-8726-7da30642d47f"
        //"c080e65f-5326-467d-9cb3-cff9d0c8f3b3"
        //"8a7cbbac-d25a-408a-a259-c09bff4eec21"
        #endregion EN_ES
        #region EN_GR
        public static readonly Dictionary<string, string> enGR = new Dictionary<string, string>
        {
            ["PathId"] = "75480967-c4a4-46ea-b26b-e301a017c3c9",
            ["CourseId"] = "53afcd3d-34c9-4871-9892-e91c1e5fe5c2",
            ["LessonId"] = "f2c86f70-fb6a-41de-8df5-ef0e953c6646"
        };
        //"c5c9058f-210b-43c2-aeeb-637de2d3b133"
        //"59b12ef4-42bb-4dfe-b79a-068551de19ae"
        //"183cea14-b1a6-4b7c-b554-ceb700b15cc3"
        #endregion EN_GR

        private static Guid LanguageCatalogId
        {
            get => new Guid(LanguageCatalogStore.LANGUAGE_CATALOG_ID);
        }
        private static Guid EnFrenchId
        {
            get => new Guid(LanguageCatalogStore.EN_FRENCH_ID);
        }
        private static Guid EnSpanishId
        {
            get => new Guid(LanguageCatalogStore.EN_SPANISH_ID);
        }
        private static Guid EnGermanId
        {
            get => new Guid(LanguageCatalogStore.EN_GERMAN_ID);
        }

        public LearningPath CreateFrenchPath(LearningLanguage french) =>
            CreateaggregateWithFirstCourseAndLesson(french,
                pathId: enFR["PathId"], firstCouresId: enFR["CourseId"],
                firstLessonId: enFR["LessonId"]);
        public LearningPath CreateSpanishPath(LearningLanguage spanish) =>
            CreateaggregateWithFirstCourseAndLesson(spanish,
                pathId: enES["PathId"], firstCouresId: enES["CourseId"],
                firstLessonId: enES["LessonId"], learningIso: IsoCodes.es);

        public LearningPath CreateGermanPath(LearningLanguage german) =>
            CreateaggregateWithFirstCourseAndLesson(german,
                pathId: enGR["PathId"], firstCouresId: enGR["CourseId"],
                firstLessonId: enGR["LessonId"], learningIso: IsoCodes.de);

        public  LearningLanguage GetFrenchLang(LanguageCatalog languageCatalog) =>
            GetFromLanguageFromId(languageCatalog, EnFrenchId);

        public LearningLanguage GetSpanishLang(LanguageCatalog languageCatalog) =>
            GetFromLanguageFromId(languageCatalog, EnSpanishId);
        public LearningLanguage GetGermanLang(LanguageCatalog languageCatalog) =>
            GetFromLanguageFromId(languageCatalog, EnGermanId);

        private static LearningLanguage GetFromLanguageFromId(LanguageCatalog languageCatalog, Guid id) =>
            languageCatalog.Items.FirstOrDefault(ll => ll.Id == new LearningLanguageId(id));

        public static LearningPath CreateaggregateWithFirstCourseAndLesson(LearningLanguage learningLanguage, string pathId = "", string firstCouresId = "", string firstLessonId = "", IsoCodes motherIso = IsoCodes.en, IsoCodes learningIso = IsoCodes.fr)
        {
            var result = CreateAggregate(learningLanguage, motherIso, AssureId(pathId));
            var couresId = result.AddCourseFromLevel(1, AssureId(firstCouresId));
            result.AddLessonToCourse(couresId, FIRST_LESSON_NAME, 1, AssureId(firstLessonId));
            return result;
        }
        private static LearningPath CreateAggregate(LearningLanguage learningLanguage, IsoCodes motherIso, string guidRaw = "") =>
            Guid.TryParse(guidRaw, out Guid guid)
                ? new LearningPath(guid, learningLanguage, Iso.CreateIso(motherIso))
                : new LearningPath(Guid.NewGuid(), learningLanguage, Iso.CreateIso(motherIso));

        private static string AssureId(string id) =>
            string.IsNullOrWhiteSpace(id)
                ? Guid.NewGuid().ToString()
                : id;
    }
}
