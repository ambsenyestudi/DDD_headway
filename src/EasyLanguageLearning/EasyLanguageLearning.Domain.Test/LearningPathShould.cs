using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using System;
using Xunit;

namespace EasyLanguageLearning.Domain.Test
{
    public class LearningPathShould
    {
        //"990ea93e-b153-46db-8a3f-de9c59696b8c"
        //"4860f206-3b4b-49da-8e4f-685fbe9240bf"
        //"7758807a-5015-47f5-b4fa-06c56b4dc400"
        private const string COURSE_NAME = "French";
        private const string FIRST_COURSE_ID = "2adf539c-e833-4fc8-b5d2-137686a39e92";
        private const string SECOND_COURSE_ID = "c023806e-82f4-42fd-81dc-3ba16e13a978";
        public Guid LEARNING_PATH_ID = Guid.NewGuid();
        public LearningPathShould()
        {
            var v = LEARNING_PATH_ID.ToString();
            var on = "";
        }
        [Fact]
        public void HaveName()
        {
            var learningPath = new LearningPath(LEARNING_PATH_ID, COURSE_NAME);
            Assert.NotEmpty(learningPath.Name);
        }
        [Fact]
        public void NotHaveTwoCoursesWithSameLevel()
        {
            var repeatedLevel = 1;
            var learningPath = new LearningPath(LEARNING_PATH_ID, COURSE_NAME);
            learningPath.AddCourseFromLevel(repeatedLevel, FIRST_COURSE_ID);
            Assert.Throws<ArgumentException>(()=>learningPath.AddCourseFromLevel(repeatedLevel, SECOND_COURSE_ID));
        }
        [Fact]
        public void HavePositiveCouresLevels()
        {
            var nonPositiveLeve = 0;
            var learningPath = new LearningPath(LEARNING_PATH_ID, COURSE_NAME);
            Assert.Throws<ArgumentException>(() => learningPath.AddCourseFromLevel(nonPositiveLeve, FIRST_COURSE_ID));
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(1, 5)]
        public void NotAllowUnorderedCourseAdding(int firstLevel, int nonOrderedLevel)
        {
            var learningPath = new LearningPath(LEARNING_PATH_ID, COURSE_NAME);
            learningPath.AddCourseFromLevel(firstLevel, FIRST_COURSE_ID);
            Assert.Throws<ArgumentException>(() => learningPath.AddCourseFromLevel(nonOrderedLevel, FIRST_COURSE_ID));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        public void HaveFirstCourseOfLevel1(int nonFirstCourse)
        {
            var learningPath = new LearningPath(LEARNING_PATH_ID, COURSE_NAME);
            Assert.Throws<ArgumentException>(() => learningPath.AddCourseFromLevel(nonFirstCourse, FIRST_COURSE_ID));
        }
    }
}
