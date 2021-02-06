using System;

namespace Courses.Domain
{
    public class CourseAggregate
    {
        private const string DuplicatedLanguageError = "Learning language cannot be the same as mother language";
        public Course ChooseACourse(string motherLanguageIsoRaw, string leaningLanguageIsoRaw)
        {
            var motherIso = IsoFromRaw(motherLanguageIsoRaw);
            var learningIso = IsoFromRaw(leaningLanguageIsoRaw);
            if(motherIso == learningIso)
            {
                throw new ArgumentException(DuplicatedLanguageError);
            }
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
            return Iso.CreateIso(isoCode);
        }
    }
}
