using EasyLanguageLearning.Domain.Evaluations.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using EasyLanguageLearning.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.API.Seeding
{
    public class SeedExercises
    {
        private static Dictionary<string, string> writingIdDictionary =
            new Dictionary<string, string>
            {
                ["Yes"] = "f933acc2-a788-4745-a68a-57acb2081362",
                ["No"] = "bd5032cd-252c-4c1f-8a27-fa3c453f6e80",
                ["Good bye"] = "252d3dcc-4a08-40c8-9177-63661e094938"
            };
        public static void Populate(DataContext dbContext)
        {
            
            foreach (var item in dbContext.VocabularyUnits)
            {
                dbContext.Remove(item);
            }
            var vocUnit = dbContext.VocabularyUnits
                .FirstOrDefault(vu => vu.Id == new VocabularyUnitId(new Guid(SeedVocabulary.EN_FR_FIRST_UNIT)));

            var exercises = CreateWritingExercises(vocUnit);

            foreach (var exercise in exercises)
            {
                dbContext.WritingExercises.Add(exercise);
            }

            dbContext.SaveChanges();


        }
        private static List<WritingExercise> CreateWritingExercises(VocabularyUnit vocUnit)
        {
            var lessonIdList = new List<LessonId> { vocUnit.LessonId };
            var eval = new Evaluation(Guid.NewGuid(), lessonIdList, vocUnit.VocabularyItems.ToList());
            var result = vocUnit.VocabularyItems.Select(voc => eval.CreateWritingExercise(voc, MapExerciseId(voc))).ToList();
            return result;
        }

        private static Guid MapExerciseId(Vocabulary voc) =>
            new Guid(writingIdDictionary[voc.MotherLanguageTerm]);
    }
}
