using Courses.Domain;
using Xunit;

namespace Courses.Tests
{
    public class CourseAggregateShould
    {
        [Fact]
        public void ChooseACourse()
        {
            var courseName = "langauge1";
            var sut = new CourseAggregate();
            var result = sut.ChooseACourse(courseName);
            Assert.NotNull(result);
        }
    }
}
