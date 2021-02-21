using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using System;
using Xunit;
using testC = EasyLanguageLearning.Domain.Test.LearningPaths.LeaningPConst;
namespace EasyLanguageLearning.Domain.Test.LearningPaths
{
    public class LearningPathShould
    {
        
        [Fact]
        public void HaveName()
        {
            var learningPath = new LearningPath(
                new Guid(testC.LEARNING_PATH_ID),
                testC.fr_LearningLanguage,
                testC.en_MotherIso);
            Assert.NotEmpty(learningPath.Name);
        }
        [Fact]
        public void NotHaveTwoCoursesWithSameLevel()
        {
            var repeatedLevel = 1;
            var learningPath = new LearningPath(
                new Guid(testC.LEARNING_PATH_ID),
                testC.fr_LearningLanguage,
                testC.en_MotherIso);
            learningPath.AddCourseFromLevel(repeatedLevel, testC.FIRST_COURSE_ID);
            Assert.Throws<ArgumentException>(()=>learningPath.AddCourseFromLevel(repeatedLevel, testC.SECOND_COURSE_ID));
        }
        [Fact]
        public void HavePositiveCouresLevels()
        {
            var nonPositiveLeve = 0;
            var learningPath = new LearningPath(
                new Guid(testC.LEARNING_PATH_ID),
                testC.fr_LearningLanguage,
                testC.en_MotherIso);
            Assert.Throws<ArgumentException>(() => learningPath.AddCourseFromLevel(nonPositiveLeve, testC.FIRST_COURSE_ID));
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(1, 5)]
        public void NotAllowUnorderedCourseAdding(int firstLevel, int nonOrderedLevel)
        {
            var learningPath = new LearningPath(
                new Guid(testC.LEARNING_PATH_ID),
                testC.fr_LearningLanguage,
                testC.en_MotherIso);
            learningPath.AddCourseFromLevel(firstLevel, testC.FIRST_COURSE_ID);
            Assert.Throws<ArgumentException>(() => learningPath.AddCourseFromLevel(nonOrderedLevel, testC.FIRST_COURSE_ID));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        public void PreventAddingOtherThanFirstWhenEmptyCourses(int nonFirstCourse)
        {
            var learningPath = new LearningPath(
                new Guid(testC.LEARNING_PATH_ID),
                testC.fr_LearningLanguage,
                testC.en_MotherIso);
            Assert.Throws<ArgumentException>(() => learningPath.AddCourseFromLevel(nonFirstCourse, testC.FIRST_COURSE_ID));
        }

        [Fact]
        public void AddFirstCourse()
        {
            var learningPath = new LearningPath(
                new Guid(testC.LEARNING_PATH_ID),
                testC.fr_LearningLanguage,
                testC.en_MotherIso);
            learningPath.AddCourseFromLevel(1, testC.FIRST_COURSE_ID);
            Assert.NotEmpty(learningPath.Courses);
        }
    }
}
