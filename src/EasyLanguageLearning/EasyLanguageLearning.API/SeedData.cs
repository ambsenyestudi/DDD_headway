using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EasyLanguageLearning.API
{
    public class SeedData
    {
        public const string LANGUAGE_CATALOG_ID = "188008a3-d7e9-48c5-890f-478d6d04e6a9";
        public const string EN_FR_LEARNING_PATH_ID = "82d83571-5fdd-40c0-ac46-0eea57a19ab0";
        
        public const string EN_FRENCH_ID = "5e91a9f4-344f-4889-8ba3-2e2195bdc9c5";
        public const string EN_SPANISH_ID = "85b36f27-76f8-4530-a941-72cdc7971ebd";
        public const string EN_GERMAN_ID = "3ea46f15-be8c-448d-b751-9563390ce9bc";
        
        public const string EN_FRENCH = "French";
        public const string EN_SPANISH = "Spanish";
        public const string EN_GERMAN = "German";
        public static void Initialize(IServiceProvider services)
        {
            using (var dbContext = new DataContext(
               services.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (!dbContext.LanguageCatalogs.Any())
                {
                    PopulateLanguageCatalogs(dbContext);
                }
                if (!dbContext.LearningPaths.Any())
                {
                    PopulateLearningPaths(dbContext);
                }
                
            }
        }

        private static void PopulateLanguageCatalogs(DataContext dbContext)
        {
            foreach (var item in dbContext.LanguageCatalogs)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();
            
            var catalog = new LanguageCatalog(new Guid(LANGUAGE_CATALOG_ID), Iso.CreateIso(IsoCodes.en));

            catalog.AddToCatalog(Iso.CreateIso(IsoCodes.fr), EN_FRENCH, new Guid(EN_FRENCH_ID));
            catalog.AddToCatalog(Iso.CreateIso(IsoCodes.es), EN_SPANISH, new Guid(EN_SPANISH_ID));
            catalog.AddToCatalog(Iso.CreateIso(IsoCodes.de), EN_GERMAN, new Guid(EN_GERMAN_ID));
            
            dbContext.LanguageCatalogs.Add(catalog);

            dbContext.SaveChanges();
        }

        public static void PopulateLearningPaths(DataContext dbContext)
        {

            foreach (var item in dbContext.LearningPaths)
            {
                dbContext.Remove(item);
            }

            dbContext.SaveChanges();

            var currLangCatalog = dbContext.LanguageCatalogs.FirstOrDefault(lc => lc.Id == new LanguageCatalogId(new Guid(LANGUAGE_CATALOG_ID)));

            var frenchLang = currLangCatalog.Items.FirstOrDefault(ll => ll.Id == new LearningLanguageId(new Guid(EN_FRENCH_ID)));
            dbContext.LearningPaths.Add(CreateaggregateWithFirstCourseAndLesson(frenchLang, EN_FR_LEARNING_PATH_ID));
            dbContext.SaveChanges();

            var EN_ES_LEARNING_PATH_ID = Guid.NewGuid().ToString();
            var spanishLang = currLangCatalog.Items.FirstOrDefault(ll => ll.Id == new LearningLanguageId(new Guid(EN_SPANISH_ID)));
            dbContext.LearningPaths.Add(CreateaggregateWithFirstCourseAndLesson(spanishLang, EN_ES_LEARNING_PATH_ID));
            dbContext.SaveChanges();

            var EN_GR_LEARNING_PATH_ID = Guid.NewGuid().ToString();
            var germanLang = currLangCatalog.Items.FirstOrDefault(ll => ll.Id == new LearningLanguageId(new Guid(EN_GERMAN_ID)));
            dbContext.LearningPaths.Add(CreateaggregateWithFirstCourseAndLesson(germanLang, EN_GR_LEARNING_PATH_ID));
            dbContext.SaveChanges();

        }

        public static LearningPath CreateaggregateWithFirstCourseAndLesson(LearningLanguage learningLanguage, string guid="", IsoCodes motherIso = IsoCodes.en)
        {
            var result = CreateAggregate(learningLanguage, motherIso, guid);
            var couresId = result.AddCourseFromLevel(1);
            result.AddLessonToCourse(couresId, "Launch pad", 1);
            return result;
        }

        private static LearningPath CreateAggregate(LearningLanguage learningLanguage, IsoCodes motherIso, string guidRaw = "") =>
            Guid.TryParse(guidRaw, out Guid guid)
                ? new LearningPath(guid, learningLanguage,Iso.CreateIso(motherIso))
                : new LearningPath(Guid.NewGuid(), learningLanguage, Iso.CreateIso(motherIso));
        
    }
}
