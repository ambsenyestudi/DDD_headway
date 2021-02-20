using System;

namespace EasyLanguageLearning.Domain.ContentSupplying.Aggregate
{
    public class Course
    {
        public LearningPathId LearningPathId { get; protected set; }
        public CourseId Id { get; protected set; }
        public string Name { get; protected set; }
        public Course(Guid id, LearningPathId learningPathId, string name)
        {
            Id = new CourseId(id);
            LearningPathId = learningPathId;
            Name = name;
        }
        protected Course()
        {
        }
        public static string NameFromLearningPath(LearningPath path, int level) =>
            path.Name +
            $" {EnsureLevelIsGreaterThanZero(level)}";

        public static int EnsureLevelIsGreaterThanZero(int level)=> 
            level < 1
                ? 1
                : level;
    }
}
