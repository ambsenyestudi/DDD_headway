using System;

namespace EasyLanguageLearning.Domain.ContentSupplying.Aggregate
{
    public class LearningPath
    {
        public LearningPathId Id { get; protected set; }
        public string Name { get; protected set; }
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
        protected virtual void UpdateId(Guid id)
        {
            Id = new LearningPathId(id);
        }
       
    }
}
