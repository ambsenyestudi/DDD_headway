using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Infrastructure;
using System;
using System.Linq;

namespace EasyLanguageLearning.API.Seeding
{
    public class SeedLearningPath
    {
        public const string EN_FR_FIRST_LESSON_ID = "e667a2bd-2f01-43f9-b796-2c322690c6f7";
        public const string EN_FR_LEARNING_PATH_ID = "82d83571-5fdd-40c0-ac46-0eea57a19ab0";
        public const string FIRST_LESSON_NAME = "Hello world!";
        public const string EN_FR_FIRST_COURSE_ID = "90b94554-5e8e-4406-9e4b-c1b90fd4cb2c";
        //"6f7567bd-55d0-4986-98f0-05ffb8fdc7d2"
        //"75480967-c4a4-46ea-b26b-e301a017c3c9"
        private static Guid LanguageCatalogId 
        {
            get => new Guid(SeedLanguageCatalog.LANGUAGE_CATALOG_ID);
        }
        private static Guid EnFrenchId
        {
            get => new Guid(SeedLanguageCatalog.EN_FRENCH_ID);
        }
        private static Guid EnSpanishId
        {
            get => new Guid(SeedLanguageCatalog.EN_SPANISH_ID);
        }
        private static Guid EnGermanId
        {
            get => new Guid(SeedLanguageCatalog.EN_GERMAN_ID);
        }
        public static void Populate(DataContext dbContext)
        {

            foreach (var item in dbContext.LearningPaths)
            {
                dbContext.Remove(item);
            }

            dbContext.SaveChanges();

            var currLangCatalog = dbContext.LanguageCatalogs.FirstOrDefault(lc => lc.Id == new LanguageCatalogId(LanguageCatalogId));
            
            var frenchLang = currLangCatalog.Items.FirstOrDefault(ll => ll.Id == new LearningLanguageId(EnFrenchId));
            dbContext.LearningPaths.Add(CreateaggregateWithFirstCourseAndLesson(frenchLang, EN_FR_LEARNING_PATH_ID));
            dbContext.SaveChanges();

            var EN_ES_LEARNING_PATH_ID = Guid.NewGuid().ToString();
            var spanishLang = currLangCatalog.Items.FirstOrDefault(ll => ll.Id == new LearningLanguageId(EnSpanishId));
            dbContext.LearningPaths.Add(CreateaggregateWithFirstCourseAndLesson(spanishLang, EN_ES_LEARNING_PATH_ID, learningIso:IsoCodes.es));
            dbContext.SaveChanges();

            var EN_GR_LEARNING_PATH_ID = Guid.NewGuid().ToString();
            var germanLang = currLangCatalog.Items.FirstOrDefault(ll => ll.Id == new LearningLanguageId(EnGermanId));
            dbContext.LearningPaths.Add(CreateaggregateWithFirstCourseAndLesson(germanLang, EN_GR_LEARNING_PATH_ID, learningIso: IsoCodes.de));
            dbContext.SaveChanges();

        }
        public static LearningPath CreateaggregateWithFirstCourseAndLesson(LearningLanguage learningLanguage, string guid = "", IsoCodes motherIso = IsoCodes.en, IsoCodes learningIso = IsoCodes.fr)
        {
            var result = CreateAggregate(learningLanguage, motherIso, guid);
            var couresId = result.AddCourseFromLevel(1, EN_FR_FIRST_COURSE_ID);
            var currLesson = (motherIso == IsoCodes.en && learningIso == IsoCodes.fr)
                ? EN_FR_FIRST_LESSON_ID
                : Guid.NewGuid().ToString();
            result.AddLessonToCourse(couresId, FIRST_LESSON_NAME, 1, currLesson);
            return result;
        }

        private static LearningPath CreateAggregate(LearningLanguage learningLanguage, IsoCodes motherIso, string guidRaw = "") =>
            Guid.TryParse(guidRaw, out Guid guid)
                ? new LearningPath(guid, learningLanguage, Iso.CreateIso(motherIso))
                : new LearningPath(Guid.NewGuid(), learningLanguage, Iso.CreateIso(motherIso));
    }
}
