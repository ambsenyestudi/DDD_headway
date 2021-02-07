using Courses.Domain;
using Courses.Domain.Exercises;
using Courses.Domain.Translations;
using Courses.Tests.Extensions;
using System;
using System.Collections.Generic;
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
            var definition = new CourseDefinition(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1);
            var root = new CourseAgregateBuilder()
                .CreateSpanishEnglishCourse(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                }, new Dictionary<Guid, string> 
                { 
                    [TC.COMIENZO_UNIT_ID] = TC.COMIENZO_UNIT_NAME 
                });
                
                
            var course = root.ChooseACourse(definition, Guid.Empty);
            root.LoadUnitContent(course, TC.COMIENZO_UNIT_ID, translationList);
            var writtenExercise = root.GetExercise(course);
            var result = writtenExercise.Evaluate(answer);
            Assert.Equal(expectedResult, result.Results);
        }
    }
}
