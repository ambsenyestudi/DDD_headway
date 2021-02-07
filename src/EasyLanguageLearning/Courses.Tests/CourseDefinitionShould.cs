using Courses.Domain;
using System;
using Xunit;

using TC = Courses.Tests.AggregateTestConstants;
namespace Courses.Tests
{
    public class CourseDefinitionShould
    {
        [Fact]
        public void NotGetCoursesWithSameLanguage()
        {
            var sameLang = TC.SPANISH_RAW_ISO;
            Assert.Throws<ArgumentException>(() => new CourseDefinition(sameLang, sameLang, 1));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public void NotGetCourseWhenLevelNotInRange1_3(int level)
        {
            Assert.Throws<ArgumentException>(() => new CourseDefinition(TC.SPANISH_RAW_ISO, TC.ENGLISH_RAW_ISO, level));
        }

        [Theory]
        [InlineData("x81", "es")]
        [InlineData("es", "x81")]
        public void NotBeCreatedWhenInvalidIsoCodes(string motherLanguage, string learningLanguage)
        {
            Assert.Throws<ArgumentException>(() => new CourseDefinition(motherLanguage, learningLanguage, level: 1));
        }
    }
}
