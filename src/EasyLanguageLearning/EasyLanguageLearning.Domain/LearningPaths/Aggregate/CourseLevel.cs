using EasyLanguageLearning.Domain.Shared.Kernel;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.LearningPaths.Aggregate
{
    public class CourseLevel: ValueObject
    {
        public int Value { get; }
        public static CourseLevel Empty { get; } = new CourseLevel(0);
        public static CourseLevel First { get; } = new CourseLevel(1);

        private CourseLevel(int level)
        {
            Value = level;
        }
        public bool IsNextLevel(CourseLevel next) =>
            next.Value - 1 == Value;

        public override string ToString() => "" + Value;

        public static CourseLevel Create(int level) =>
            IsPositiveLevel(level)
            ? new CourseLevel(level)
            : CourseLevel.Empty;

        public static bool IsPositiveLevel(int level) => 
            level > 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
