using Courses.Domain;
using Courses.Domain.Exercises;
using Courses.Domain.Translations;
using Courses.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TC = Courses.Tests.AggregateTestConstants;

namespace Courses.Tests
{
    public class MultipleChoiceTest
    {
        [Fact]
        public void TellWhenAnswerIsRight()
        {
            var expectedResult = TestResults.Right;

            var translationList = new List<Translation>
            {
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Si", "Yes"),
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "No", "No"),
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Hola", "Hello"),
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Adiós", "Good Bye")
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
            
            var multiplChoiceExercise = root.GetMultipleChoiceExercise(course);
        }
    }
}
