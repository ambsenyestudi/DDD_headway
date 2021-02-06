using Courses.Domain;
using System;
using Xunit;

namespace Courses.Tests
{
    public class CourseAggregateShould
    {
        [Fact]
        public void ChooseACourse()
        {
            var motherLanguage = "es";
            var learningLanguage = "en";
            var sut = new CourseAggregate();
            var result = sut.ChooseACourse(motherLanguage, learningLanguage);
            Assert.NotNull(result);
        }
        [Fact]
        public void GetNotGetCoursesWithSameLanguage()
        {
            var motherLanguage = "es";
            var learningLanguage = "es";
            var sut = new CourseAggregate();
            Assert.Throws<ArgumentException>(()=> sut.ChooseACourse(motherLanguage, learningLanguage));
        }
    }
}
