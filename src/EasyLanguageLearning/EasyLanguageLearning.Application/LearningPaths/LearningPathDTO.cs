using EasyLanguageLearning.Application.LanguageCatalog;
using System;

namespace EasyLanguageLearning.Application.LearningPaths
{
    public class LearningPathDTO
    {
        public Guid Id { get; set; }
        public LearningLanguageDTO LearningLanguage { get; set; }
        public CourseDTO[] CourseList { get; set; }
    }
}
