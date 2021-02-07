using EasyLanguageLearning.Domain.Shared.Kernel;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;

namespace Courses.Domain
{
    public class CourseDefinition:ValueObject
    {

        private const string LOW_LEVEL_ERROR = "level cannot be lower than";
        private const string HIGH_LEVEL_ERROR = "level cannot be higher than";
        private const string InvalidLanguageError = "Invalid language Iso";
        private const string DUPLICATED_LANGUAGE_ERROR = "Mother langage and Learning language cannot be the same";

        private const int MIN_LEVEL = 1;
        private const int MAX_LEVEL = 3;
        
        public Iso MotherLanguageIso { get; }
        public Iso LeaningLanguageIso { get; }
        public int Level { get; set; }
        
        public CourseDefinition(string motherLanguageIsoRaw, string leaningLanguageIsoRaw, int level)
        {
            MotherLanguageIso = IsoFromRaw(motherLanguageIsoRaw);
            LeaningLanguageIso = IsoFromRaw(leaningLanguageIsoRaw);
            Level = level;
            EnsureValidCourseDefintion();
        }
        protected override IEnumerable<object> GetEqualityComponents() =>
            new object[] { MotherLanguageIso, LeaningLanguageIso, Level };

        private void EnsureValidCourseDefintion()
        {
            EnsureNotSameLanguage(MotherLanguageIso, LeaningLanguageIso);
            EnsureLevelInRange(Level);
        }

        

        private void EnsureLevelInRange(int level)
        {
            if (level < MIN_LEVEL)
            {
                throw new ArgumentException($"{LOW_LEVEL_ERROR} {MIN_LEVEL}");
            }
            if (level > MAX_LEVEL)
            {
                throw new ArgumentException($"{HIGH_LEVEL_ERROR} {MAX_LEVEL}");
            }
        }
        private Iso IsoFromRaw(string isoCodeRaw)
        {
            var isoCode = ParseIsoCode(isoCodeRaw);
            var result = Iso.CreateIso(isoCode);

            return result == Iso.Empty
                ? throw new ArgumentException(InvalidLanguageError)
                : result;
        }
        private IsoCodes ParseIsoCode(string isoCodeRaw)
        {
            if (Enum.TryParse(isoCodeRaw, out IsoCodes parsedIso))
            {
                return parsedIso;
            }
            return IsoCodes.None;
        }
        private void EnsureNotSameLanguage(Iso motherIso, Iso learningIso)
        {
            if (motherIso == learningIso)
            {
                throw new ArgumentException(DUPLICATED_LANGUAGE_ERROR);
            }
        }
    }
}
