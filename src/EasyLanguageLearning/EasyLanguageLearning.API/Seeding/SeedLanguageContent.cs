using EasyLanguageLearning.Domain.LanguageContents;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Infrastructure;
using System;

namespace EasyLanguageLearning.API.Seeding
{
    public static class SeedLanguageContent
    {
        
        public static void Populate(DataContext dbContext)
        {
            var lessonId = new LessonId(new Guid(SeedLearningPath.EN_FR_FIRST_LESSON_ID));
            foreach (var item in dbContext.LanguageContents)
            {
                dbContext.Remove(item);
            }
            var oui = TranslateEnFr(
                "Yes", "Oui", lessonId);
            dbContext.LanguageContents.Add(oui);
            dbContext.SaveChanges();
        }

        private static LanguageContent TranslateEnFr(string enStr, string frStr, LessonId lessonId)
        {

            var trans = TranslatedContent.Create(
               Iso.CreateIso(IsoCodes.en),
               enStr,
               Iso.CreateIso(IsoCodes.fr),
               frStr);
            return new LanguageContent(Guid.NewGuid(), lessonId, trans);
        }
    }
}
