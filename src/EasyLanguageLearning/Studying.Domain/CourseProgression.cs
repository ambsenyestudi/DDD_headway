using Courses.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Studying.Domain
{
    internal class CourseProgression
    {
        internal CourseId Course { get; }
        private readonly List<UnitProgression> unitList;

        internal CourseProgression(CourseId course, List<UnitProgression> units)
        {
            this.Course = course;
            this.unitList = units;
        }
        internal float GetCompletionPercentaje() =>
            unitList.Sum(u => u.GetCompletionPercentaje()) /
            (float)unitList.Count;
    }
}