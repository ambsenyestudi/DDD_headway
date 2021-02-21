using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using System;
using System.Linq;
using Xunit;
using testC = EasyLanguageLearning.Domain.Test.LearningPaths.LeaningPConst;
namespace EasyLanguageLearning.Domain.Test.LearningPaths
{
    public class CourseShould
    {
        [Fact]
        public void AddFirstLesson()
        {
            var learningPath = new LearningPath(
                new Guid(testC.LEARNING_PATH_ID),
                testC.LEARNING_PATH_NAME);
            learningPath.AddCourseFromLevel(1, testC.FIRST_COURSE_ID);
            var firstCourse = learningPath.Courses.First();
            firstCourse.AddLesson(testC.FIRST_LESSON_NAME, 1);
            Assert.NotEmpty(firstCourse.Lessons);
        }

        [Fact]
        public void NotHaveTwoCoursesWithSameLevel()
        {
            var repeatedLevel = 1;
            var learningPath = new LearningPath(
                new Guid(testC.LEARNING_PATH_ID),
                testC.LEARNING_PATH_NAME);
            learningPath.AddCourseFromLevel(repeatedLevel, testC.FIRST_COURSE_ID);
            var firstCourse = learningPath.Courses.First();
            firstCourse.AddLesson(testC.FIRST_LESSON_NAME, repeatedLevel);
            Assert.Throws<ArgumentException>(() => firstCourse.AddLesson("", repeatedLevel));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        public void PreventAddingOtherThanFirstWhenEmptyCourses(int nonFirstCourse)
        {
            var learningPath = new LearningPath(
                new Guid(testC.LEARNING_PATH_ID),
                testC.LEARNING_PATH_NAME);
            learningPath.AddCourseFromLevel(1, testC.FIRST_COURSE_ID);
            var firstCourse = learningPath.Courses.First();
            Assert.Throws<ArgumentException>(() => firstCourse.AddLesson(testC.FIRST_LESSON_NAME, nonFirstCourse));
        }
    }
}
