using System;

namespace EasyLanguageLearning.Domain.LearningPaths.Aggregate
{
    public class Lesson
    {
        public LessonId Id { get; protected set; }
        public string Name { get; protected set; }
        public Level Level { get; protected set; }

        protected Lesson()
        {
        }

        internal Lesson(Guid id, string name, Level level)
        {
            Id = new LessonId(id);
            Name = name;
            Level = level;
        }
    }
}
