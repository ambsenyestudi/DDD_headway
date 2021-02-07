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
        public void TellWhenAnswerIsRight()
        {
            var expectedResult = TestResults.Right;

            var answer = "Yes";
            
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
            var writtenExercise = root.CreateWrittingExercise(course);
            var result = writtenExercise.Evaluate(answer);
            Assert.Equal(expectedResult, result.Results);
        }

        [Fact]
        public void TellWhenAnswerIsAlmostRight()
        {
            var expectedResult = TestResults.Almost;
            
            var testAnswer = "Yes";
            var partialAnswer = testAnswer.Substring(0, 2);
                        
            var translationList = new List<Translation>
            {
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Si", testAnswer)
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
            var writtenExercise = root.CreateWrittingExercise(course);
            var result = writtenExercise.Evaluate(partialAnswer);
            Assert.Equal(expectedResult, result.Results);
        }

        [Fact]
        public void TellWhenAnswerIsWrong()
        {
            var expectedResult = TestResults.Wrong;

            var wrongAnswer = "1234567###";

            var translationList = new List<Translation>
            {
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Si", "Yes")
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
            var writtenExercise = root.CreateWrittingExercise(course);
            var result = writtenExercise.Evaluate(wrongAnswer);
            Assert.Equal(expectedResult, result.Results);
        }

        [Theory]
        [InlineData("Hel", "Hell")]
        [InlineData("Hol", "He")]
        public void AddACorrectLettreToTheRightPartOfYourAnswer(string currAnswer, string expectedTip)
        {
            var translationList = new List<Translation>
            {
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Hola", "Hello")
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
            var writtenExercise = root.CreateWrittingExercise(course);
            var result = writtenExercise.GetTip(currAnswer);

            Assert.Equal(expectedTip, result);
        }
    }
}
