using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;

namespace Courses.Tests
{
    public static class AggregateTestConstants
    {
        public const IsoCodes SPANISH_ISO_CODE = IsoCodes.es;
        public const IsoCodes ENGLISH_ISO_CODE = IsoCodes.en;
        public const string COMIENZO_UNIT_NAME = "Comienzo";
        public static readonly Guid COMIENZO_UNIT_ID = new Guid("eec6b9bc-441a-45f0-8ea7-7ca2e244991c");
        public static readonly string SPANISH_RAW_ISO = SPANISH_ISO_CODE.ToString();
        public static readonly string ENGLISH_RAW_ISO = ENGLISH_ISO_CODE.ToString();
        public static readonly Iso SPANISH_ISO = Iso.CreateIso(SPANISH_ISO_CODE);
        public static readonly Iso ENGLISH_ISO = Iso.CreateIso(ENGLISH_ISO_CODE);
        public static readonly Iso FRENCH_ISO = Iso.CreateIso(IsoCodes.fr);
    }
}
