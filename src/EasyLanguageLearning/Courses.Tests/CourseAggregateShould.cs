using Courses.Domain;
using Courses.Tests.Extensions;
using System;
using System.Collections.Generic;
using Xunit;

using TC = Courses.Tests.AggregateTestConstants;
namespace Courses.Tests
{
    public class CourseAggregateShould
    {
        
        [Fact]
        public void ChooseACourse()
        {
            var definition = new CourseDefinition(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1);
            var sut = new CourseAgregateBuilder()
                .CreateSpanishEnglishCourse(new Dictionary<string, string>
                {
                    ["English"] = "Inglés"
                });
            var result = sut.ChooseACourse(definition, Guid.Empty);
            Assert.NotNull(result);
        }
        
        

        [Fact]
        public void GetCourseNameInMotherLangauge()
        {
            var definition = new CourseDefinition(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, 1);
            var exptected = "Inglés";
            var sut = new CourseAgregateBuilder()
               .CreateSpanishEnglishCourse(new Dictionary<string, string>
               {
                   ["English"] = exptected
               });
            var result = sut.ChooseACourse(definition, Guid.Empty);
            Assert.Contains(exptected, result.Name);
        }
        
    }
}
