using System;

namespace EasyLanguageLearning.Domain.LearningPaths.Aggregate
{
    public class Course
    {
        #region EF
        //ef forces this to be a public property may protected field?
        public string PathName { get; set; }
        #endregion EF
        public CourseLevel Level { get; protected set; }
        public LearningPathId LearningPathId { get; protected set; }
        public CourseId Id { get; protected set; }        
        public Course(Guid id, LearningPathId learningPathId, CourseName name)
        {
            Id = new CourseId(id);
            LearningPathId = learningPathId;
            Level = name.Level;
            PathName = name.PathName;
        }
        protected Course()
        {
        }

        public CourseName GetName() =>
            CourseName.Create(PathName, Level.Value);
    }
}
