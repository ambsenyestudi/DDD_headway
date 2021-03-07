using System;

namespace EasyLanguageLearning.Application.LearningPaths
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public LessonDTO[] LessonList { get; set; }
    }
}
