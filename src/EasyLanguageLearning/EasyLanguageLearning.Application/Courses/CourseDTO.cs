using Courses.Domain;
using System;

namespace EasyLanguageLearning.Application.Courses
{
    public class CourseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string MotherLanguage { get; set; }
        public string LearningLanguage { get; set; }
        public string MotherLanguageIso { get; set; }
        public string LearningLanguageIso { get; set; }

        internal static CourseDTO FromCourse(Course cu) =>
            new CourseDTO
            {
                Id = cu.Id.Value,
                Name = cu.Name
            };
    }
}
