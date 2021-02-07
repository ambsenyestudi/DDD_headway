using Courses.Domain;
using Courses.Domain.Exceptions;
using Courses.Domain.Translations;
using Courses.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TC = Courses.Tests.AggregateTestConstants;

namespace Courses.Tests
{
    public class UnitShould
    {
        [Fact]
        public void NotHaveRepeatedContent()
        {
            var repeatdTranslationList = new List<Translation>
            {
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Si","Yes"),
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Si","Yes"),
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

            Assert.Throws<RepeatedUnitContentException>(() => root.LoadUnitContent(course, TC.COMIENZO_UNIT_ID, repeatdTranslationList));
        }
        [Fact]
        public void LoadAllUnitContent()
        {
            var translationList = new List<Translation>
            {
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "Si","Yes"),
                Translation.Create(TC.SPANISH_ISO, TC.ENGLISH_ISO, "No","No"),
            };
            var exptectedTranslationCount = translationList.Count;

            var definition = new CourseDefinition(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1);
            
            var root = new CourseAgregateBuilder()
                .CreateSpanishEnglishCourse(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                }, new Dictionary<Guid, string> { 
                    [TC.COMIENZO_UNIT_ID] = TC.COMIENZO_UNIT_NAME 
                });

            var course = root.ChooseACourse(definition, Guid.Empty);
            root.LoadUnitContent(course, TC.COMIENZO_UNIT_ID, translationList);
            var actualCount = course.UnitList.First().Content.Count;

            Assert.Equal(exptectedTranslationCount, actualCount);
        }

        [Fact]
        public void HaveAllContentInLanguage()
        {
            var translationList = new List<Translation>
            {
                Translation.Create(TC.SPANISH_ISO, TC.FRENCH_ISO, "Si","Oui"),
                Translation.Create(TC.FRENCH_ISO, TC.ENGLISH_ISO, "Non","No"),
            };

            var definition = new CourseDefinition(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1);

            var root = new CourseAgregateBuilder()
                .CreateSpanishEnglishCourse(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                }, new Dictionary<Guid, string> { 
                    [TC.COMIENZO_UNIT_ID] = TC.COMIENZO_UNIT_NAME 
                });

            var course = root.ChooseACourse(definition, Guid.Empty);

            Assert.Throws<InvalidUnitContentException>(() => root.LoadUnitContent(course, TC.COMIENZO_UNIT_ID, translationList));
        }
    }
}
