using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLanguageLearning.Domain.LearningPaths.Aggregate
{
    public class Course
    {
        #region EF
        //ef forces this to be a public property may protected field?
        public string PathName { get; set; }
        protected Course()
        {
        }
        #endregion EF
        public Level Level { get; protected set; }
        public LearningPathId LearningPathId { get; protected set; }
        public CourseId Id { get; protected set; }
        public ICollection<Lesson> Lessons { get; private set; } = new List<Lesson>();
        public Course(Guid id, LearningPathId learningPathId, CourseName name)
        {
            Id = new CourseId(id);
            LearningPathId = learningPathId;
            Level = name.Level;
            PathName = name.PathName;
        }
        
        public CourseName GetName() =>
            CourseName.Create(PathName, Level.Value);

        public void AddLesson(string name, int level, string guidId = "")
        {
            if (!Guid.TryParse(guidId, out Guid lessonId))
            {
                lessonId = Guid.NewGuid();
            }
            Level.EnsurePostiveLevel(level);

            var levelList = Lessons.Select(x => x.Level).ToList();
            var currLevel = Level.Create(level);

            currLevel.EnsureNotRepeated(levelList);
            currLevel.EnsureOrderlyFashon(levelList);

            var currLesson = new Lesson(lessonId, name, Level.Create(level), Id);
            Lessons.Add(currLesson);
        }

    }
}
