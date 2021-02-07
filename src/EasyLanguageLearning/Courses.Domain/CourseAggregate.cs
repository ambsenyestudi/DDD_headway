

using Courses.Domain.Exercises;
using Courses.Domain.Languages;
using Courses.Domain.Translations;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;

namespace Courses.Domain
{
    public class CourseAggregate
    {
        private const string DuplicatedLanguageError = "Learning language cannot be the same as mother language";
        
        private const string LanguageNotInCatalogError = "Language Iso not present in catalog";
        private readonly ILanguageLookUp languageLookUp;
        private readonly ITranslationLookUp translationLookUp;
        private readonly IUnitLookUp unitLookUp;

        public CourseAggregate(ILanguageLookUp languageLookUp, ITranslationLookUp translationLookUp, IUnitLookUp unitLookUp)
        {
            this.languageLookUp = languageLookUp;
            this.translationLookUp = translationLookUp;
            this.unitLookUp = unitLookUp;
        }
        public Course ChooseACourse(CourseDefinition definition, Guid courseId)
        {
            
            EnsureLanguagesInCatalog(definition.MotherLanguageIso, definition.LeaningLanguageIso);
            
            var course = CouresFromIso(courseId, definition.MotherLanguageIso, definition.LeaningLanguageIso);
            course.SetName(definition.Level,translationLookUp);

            course.LoadUnits(unitLookUp.GetUnits(courseId));

            return course;
        }

        /// <summary>
        /// Factory method for writting exercises
        /// </summary>
        /// <param name="course">source of the content to be teste</param>
        /// <param name="isFixedRandom">optional for unit test radom purposes</param>
        /// <returns>WrittingExercise</returns>
        public WrittingExercise CreateWrittingExercise(Course course, bool isFixedRandom = false) =>
            course.CreateWrittingExercise(isFixedRandom);

        /// <summary>
        /// Factory method for multiple choice exercises
        /// </summary>
        /// <param name="course">source of the content to be teste</param>
        /// <param name="isFixedRandom">optional for unit test radom purposes</param>
        /// <returns>MultipleChoiceExercise</returns>
        public MultipleChoiceExercise GetMultipleChoiceExercise(Course course, bool isFixedRandom = false) =>
            course.CreateMultipleChoiceExercise(isFixedRandom);

        public Course LoadUnitContent(Course course, Guid unitId, List<Translation> content)
        {
            
            course.LoadUnitContent(unitId, content);
            return course;
        }

        private void EnsureLanguagesInCatalog(Iso motherIso, Iso lanaguagIso)
        {
            if (!languageLookUp.CatalogContains(motherIso) ||
                !languageLookUp.CatalogContains(lanaguagIso))
            {
                throw new ArgumentException(LanguageNotInCatalogError);
            }
        }
        
        private Course CouresFromIso(Guid id, Iso motherIso, Iso learningIso)
        {
            var motherLanguage = languageLookUp.GetLanguage(motherIso);
            var learningLanguage = languageLookUp.GetLanguage(learningIso);
            return new Course(id, motherLanguage, learningLanguage);
        }
    }
}
