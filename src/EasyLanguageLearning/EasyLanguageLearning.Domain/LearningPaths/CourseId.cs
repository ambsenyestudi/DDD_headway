using EasyLanguageLearning.Domain.Shared.Kernel;
using System;

namespace EasyLanguageLearning.Domain.LearningPaths
{
    public class CourseId : ValueId
    {
        public CourseId(Guid id) : base(id)
        {
        }
    }
}
