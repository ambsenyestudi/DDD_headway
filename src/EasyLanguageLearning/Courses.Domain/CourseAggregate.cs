using System;

namespace Courses.Domain
{
    public class CourseAggregate
    {
        public Course ChooseACourse(string motherLanguageIsoRaw, string leaningLanguageIsoRaw)
        {
            var motherIso = IsoFromRaw(motherLanguageIsoRaw);
            var learningIso = IsoFromRaw(leaningLanguageIsoRaw);
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
