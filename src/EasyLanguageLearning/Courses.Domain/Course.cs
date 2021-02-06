using Courses.Domain.Languages;
using Courses.Domain.Translations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Domain
{
    public class Course
    {

        private const int MIN_LEVEL = 1;
        private const int MAX_LEVEL = 3;
        private const string LOW_LEVEL_ERROR = "level cannot be lower than";
        private const string HIGH_LEVEL_ERROR = "level cannot be higher than";
        private const string INVALID_UNIT_ERROR = "suplied unit does not belong to current coures";
        public string Name { get; protected set; }
        public Guid Id { get; protected set; }
        public Language MotherLanguage { get; protected set; }
        public Language LearningLanguage { get; protected set; }
        private IEnumerable<Unit> unitCollection;
        public List<Unit> UnitList { get => unitCollection.ToList(); }

            
        public Course(Guid id, Language motherLanguae, Language learningLanguage)
        {
            Id = id;
            MotherLanguage = motherLanguae;
            LearningLanguage = learningLanguage;;
        }
        public void SetName(int level, ITranslationLookUp translation)
        {
            EnsureLevelInRange(level);
            var translatedName = translation
                .Translate(LearningLanguage.Iso, MotherLanguage.Iso, LearningLanguage.Name);
            Name = $"{translatedName} {level}";
        }

        public void LoadUnitContent(Guid unitId, List<Translation> content)
        {
            if(!unitCollection.Any(u=>u.Id == unitId))
            {
                throw new ArgumentException(INVALID_UNIT_ERROR);
            }
            var unitList = UnitList;
            var currUnit = unitList.First(u => u.Id == unitId);
            currUnit.LoadContent(content);
            unitCollection = unitList;

        }

        public void LoadUnits(IEnumerable<Unit> unitCollection)
        {
            this.unitCollection = unitCollection;
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
