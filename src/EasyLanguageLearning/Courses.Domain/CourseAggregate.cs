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
        private const string InvalidLanguageError = "Invalid language Iso";
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
        public Course ChooseACourse(string motherLanguageIsoRaw, string leaningLanguageIsoRaw, int level, Guid courseId)
        {
            var motherIso = IsoFromRaw(motherLanguageIsoRaw);
            var learningIso = IsoFromRaw(leaningLanguageIsoRaw);
            EnsureNotSameLanguage(motherIso, learningIso);
            EnsureLanguagesInCatalog(motherIso, learningIso);
            
            var course = CouresFromIso(courseId, motherIso, learningIso);
            course.SetName(level, translationLookUp);

            course.LoadUnits(unitLookUp.GetUnits(courseId));

            return course;
        }
        
        public Course LoadUnitContent(Course course, Guid unitId, List<Translation> content)
        {
            
            course.LoadUnitContent(unitId, content);
            return course;
        }


        private IsoCodes ParseIsoCode(string isoCodeRaw)
        {
            if(Enum.TryParse(isoCodeRaw, out IsoCodes parsedIso))
            {
                return parsedIso;
            }
            return IsoCodes.None;
        }
        private Iso IsoFromRaw(string isoCodeRaw)
        {
            var isoCode = ParseIsoCode(isoCodeRaw);
            var result = Iso.CreateIso(isoCode);

            return result == Iso.Empty
                ? throw new ArgumentException(InvalidLanguageError)
                : result;
        }

        private void EnsureLanguagesInCatalog(Iso motherIso, Iso lanaguagIso)
        {
            if (!languageLookUp.CatalogContains(motherIso) ||
                !languageLookUp.CatalogContains(lanaguagIso))
            {
                throw new ArgumentException(DuplicatedLanguageError);
            }
        }
        private void EnsureNotSameLanguage(Iso motherIso, Iso learningIso)
        {
            if (motherIso == learningIso)
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
