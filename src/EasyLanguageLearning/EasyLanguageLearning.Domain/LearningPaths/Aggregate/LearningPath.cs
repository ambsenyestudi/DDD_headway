using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.Domain.LearningPaths.Aggregate
{
    public class LearningPath
    {
        public LearningPathId Id { get; protected set; }
        public string Name { get; protected set; }
        public Iso LearningLanguageIso { get; protected set; }
        public Iso MotherLanguageIso { get; protected set; }
        public ICollection<Course> Courses { get; private set; } = new List<Course>();
        //maybe override collections to set pathName
        protected LearningPath()
        {
        }
        public LearningPath(Guid id, LearningLanguage learningLanguage, Iso motherLanguageIso)
        {
            Id = new LearningPathId(id);
            Name = learningLanguage.Name;
            LearningLanguageIso = learningLanguage.Iso;
            MotherLanguageIso = motherLanguageIso;
        }

        public CourseId AddCourseFromLevel(int level, string id = "")
        {
            Level.EnsurePostiveLevel(level);
            var levelList = Courses.Select(x => x.Level).ToList();
            var currLevel = Level.Create(level);

            currLevel.EnsureNotRepeated(levelList);
            currLevel.EnsureOrderlyFashon(levelList);

            if (!Guid.TryParse(id, out Guid courseId))
            {
                courseId = Guid.NewGuid();
            }
            var courseName = CourseName.Create(Name, level);
                        
            var newCourse = new Course(courseId, Id, courseName);
            Courses.Add(newCourse);
            return newCourse.Id;
        }
        public void AddLessonToCourse(CourseId courseId, string name, int level, string guidId = "")
        {
            var course = Courses.FirstOrDefault(co => co.Id == courseId);
            course.AddLesson(name, level, guidId);
        }

    }
}
