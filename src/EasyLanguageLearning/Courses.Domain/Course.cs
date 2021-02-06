using Courses.Domain.Translations;
using System;

namespace Courses.Domain
{
    public class Course
    {
        private const int MIN_LEVEL = 1;
        private const int MAX_LEVEL = 3;
        private const string LOW_LEVEL_ERROR = "level cannot be lower than";
        private const string HIGH_LEVEL_ERROR = "level cannot be higher than";
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
            EnsureLevelInRange(level);
            var translatedName = translation
                .Translate(LearningLanguage.Iso, MotherLanguage.Iso, LearningLanguage.Name);
            Name = $"{translatedName} {level}";
        }

        private void EnsureLevelInRange(int level)
        {
            if (level < MIN_LEVEL)
            {
                throw new ArgumentException($"{LOW_LEVEL_ERROR} {MIN_LEVEL}");
            }
            if(level > MAX_LEVEL)
            {
                throw new ArgumentException($"{HIGH_LEVEL_ERROR} {MAX_LEVEL}");
            }
        }
    }
}
