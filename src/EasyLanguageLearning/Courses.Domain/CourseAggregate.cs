using System;

namespace Courses.Domain
{
    public class CourseAggregate
    {
        private const string DuplicatedLanguageError = "Learning language cannot be the same as mother language";
        private const string InvalidLanguageError = "Invalid language Iso";
        private const string LanguageNotInCatalogError = "Language Iso not present in catalog";
        private readonly LanguageCatalog languageCatalog;

        public CourseAggregate(LanguageCatalog languageCatalog)
        {
            this.languageCatalog = languageCatalog;
        }
        public Course ChooseACourse(string motherLanguageIsoRaw, string leaningLanguageIsoRaw)
        {
            var motherIso = IsoFromRaw(motherLanguageIsoRaw);
            var learningIso = IsoFromRaw(leaningLanguageIsoRaw);
            EnsureNotSameLanguage(motherIso, learningIso);
            EnsureLanguagesInCatalog(motherIso, learningIso);
            
            return new Course(motherIso, learningIso);
        }

        private IsoCodes ParseIsoCode(string isoCodeRaw)
        {
            if(Enum.TryParse<IsoCodes>(isoCodeRaw, out IsoCodes parsedIso))
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
            if (!languageCatalog.Contains(motherIso) ||
                !languageCatalog.Contains(lanaguagIso))
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
    }
}
