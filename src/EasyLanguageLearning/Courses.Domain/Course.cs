using Courses.Domain.Exceptions;
using Courses.Domain.Languages;
using Courses.Domain.Translations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Domain
{
    public class Course
    {
        private const string INVALID_UNIT_ERROR = "suplied unit does not belong to current coures";

        public CourseId Id { get; protected set; }
                
        public string Name { get; protected set; }
        
        public Language MotherLanguage { get; protected set; }
        public Language LearningLanguage { get; protected set; }
        private IEnumerable<Unit> unitCollection;

        public List<Unit> UnitList { get => unitCollection.ToList(); }
    
        internal Course(CourseId id, Language motherLanguae, Language learningLanguage)
        {
            Id = id;
            MotherLanguage = motherLanguae;
            LearningLanguage = learningLanguage;;
        }

        internal void SetName(int level, ITranslationLookUp translation)
        {
            var translatedName = translation
                .Translate(LearningLanguage.Iso, MotherLanguage.Iso, LearningLanguage.Name);
            Name = $"{translatedName} {level}";
        }

        internal void LoadUnitContent(Guid unitId, List<Translation> content)
        {
            if(!unitCollection.Any(u=>u.Id == unitId))
            {
                throw new ArgumentException(INVALID_UNIT_ERROR);
            }
            EnsureValidCourseContent(content);
            var unitList = UnitList;
            var currUnit = unitList.First(u => u.Id == unitId);
            currUnit.LoadContent(content);
            unitCollection = unitList;

        }
                        

        internal void LoadUnits(IEnumerable<Unit> unitCollection)
        {
            this.unitCollection = unitCollection;
        }

        private void EnsureValidCourseContent(List<Translation> content)
        {
            if (content.Any(c => c.From != MotherLanguage.Iso) || content.Any(c => c.To != LearningLanguage.Iso))
            {
                throw new InvalidUnitContentException(InvalidUnitContentException.INVALID_CONTENT_LANGUAGE_ERROR);
            }
        }

        private Unit GetRandomUnit()
        {
            var randomIndex = new Random().Next(0, UnitList.Count - 1);
            return UnitList[randomIndex];
        }

    }
}
