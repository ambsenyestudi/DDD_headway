using EasyLanguageLearning.Domain.Shared.Kernel;
using System;

namespace EasyLanguageLearning.Domain.LearningPaths
{
    public class LessonId : ValueId
    {
        public LessonId(Guid id) : base(id)
        {
        }
    }
}
