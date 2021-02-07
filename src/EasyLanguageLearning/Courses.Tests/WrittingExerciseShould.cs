using Courses.Domain.Exercises;
using Courses.Domain.Translations;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TC = Courses.Tests.AggregateTestConstants;

namespace Courses.Tests
{
    public class WrittingExerciseShould
    {
        [Fact]
        public void GetRightAnswerWhenCorrect()
        {
            var answer = "Yes";
            var expectedResult = TestResults.Right;
            var translationList = new List<Translation>
            {
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Si", answer)
            };
            var exptectedTranslationCount = translationList.Count;
            var root = new CourseAgregateBuilder()
                .WithLanguagesInCatalog(new Dictionary<IsoCodes, string>
                {
                    [TC.SPANISH_ISO_CODE] = "Español",
                    [TC.ENGLISH_ISO_CODE] = "English"
                })
                .WithTranslations(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                })
                .WithUnits(new Dictionary<Guid, string> { [TC.COMIENZO_UNIT_ID] = TC.COMIENZO_UNIT_NAME })
                .Build();
            var course = root.ChooseACourse(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1, Guid.Empty);
            root.LoadUnitContent(course, TC.COMIENZO_UNIT_ID, translationList);
            var writtenExercise = root.GetExercise(course);
            var result = writtenExercise.Evaluate(answer);
            Assert.Equal(expectedResult, result.Results);
        }
    }
}
