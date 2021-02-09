using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel;
using System.Collections.Generic;

namespace Studying.Domain
{
    public interface ICourseLookup
    {
        List<UnitId> GetUnits(CourseId courseId);
        CourseId GetCourse(LearningPathDefinition leraningPathDefinition);
    }
}
