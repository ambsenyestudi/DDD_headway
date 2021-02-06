using Courses.Domain.Translations;
using System;

namespace Courses.Domain
{
    public class Course
    {
        public string Name { get; protected set; }
        public Guid Id { get; protected set; }
        public Language MotherLanguage { get; protected set; }
        public Language LearningLanguage { get; protected set; }
        protected Course()
        {

        }
            
        public Course(Guid id, Language motherLanguae, Language learningLanguage)
        {
            this.Id = id;
            this.MotherLanguage = motherLanguae;
            this.LearningLanguage = learningLanguage;;
        }

        public void SetName(int level, ITranslationLookUp translation)
        {
            Name = translation.Translate(LearningLanguage.Iso, MotherLanguage.Iso, LearningLanguage.Name);
        }

    }
}
