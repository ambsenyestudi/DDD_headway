using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.ContentSupplying.Aggregate
{
    public class LearningPath
    {
        public LearningPathId Id { get; protected set; }
        public string Name { get; protected set; }
        public ICollection<Course> Courses { get; private set; } = new List<Course>();
        protected LearningPath()
        {
        }
        internal LearningPath(Guid id)
        {
            UpdateId(id);
        }
        public virtual void UpdatName(string name)
        {
            //todo domain event
            Name = name;
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }

        protected virtual void UpdateId(Guid id)
        {
            Id = new LearningPathId(id);
        }
       
    }
}
