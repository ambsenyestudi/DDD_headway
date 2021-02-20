using EasyLanguageLearning.Domain.Shared.Kernel;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.LearningPaths.Aggregate
{
    public class CourseName: ValueObject
    {
        public string PathName { get;}
        public CourseLevel Level { get; }
        public static CourseName Empty { get; } = new CourseName(string.Empty, CourseLevel.Empty);

        private CourseName(string pathName, CourseLevel level)
        {
            PathName = pathName;
            Level = level;
        }

        public static CourseName Create(string pathName, int level) =>
            CourseLevel.IsPositiveLevel(level)
            ? new CourseName(pathName, CourseLevel.Create(level))
            : CourseName.Empty;


        public override string ToString() =>
            $"{PathName} {Level.Value}";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ToString();
        }
    }
}
