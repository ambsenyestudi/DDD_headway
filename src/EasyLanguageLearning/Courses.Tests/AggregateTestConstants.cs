using EasyLanguageLearning.Domain.Shared.Kernel.Languages;

namespace Courses.Tests
{
    public static class AggregateTestConstants
    {
        public const IsoCodes SPANISH_ISO_CODE = IsoCodes.es;
        public const IsoCodes ENGLISH_ISO_CODE = IsoCodes.en;
        public static readonly string SPANISH_RAW_ISO = SPANISH_ISO_CODE.ToString();
        public static readonly string ENGLISH_RAW_ISO = ENGLISH_ISO_CODE.ToString();
    }
}
