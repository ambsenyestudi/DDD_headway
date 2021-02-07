using EasyLanguageLearning.Domain.Shared.Kernel;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Collections.Generic;

namespace Courses.Domain.Translations
{
    public class Translation: ValueObject
    {
        public static Translation Empty { get; } = new Translation(
            Iso.CreateIso(IsoCodes.None),
            Iso.CreateIso(IsoCodes.None),
            string.Empty,
            string.Empty);
        public Iso From { get; }
        public Iso To { get; }
        public string OriginalTerm { get; }
        public string TranslatedTerm { get; }
        protected Translation(Iso from, Iso to, string originalTerm, string translatedTerm)
        {
            From = from;
            To = to;
            OriginalTerm = originalTerm;
            TranslatedTerm = translatedTerm;
        }

        public static Translation Create(Iso from, Iso to, string originalTerm, ITranslationLookUp translationLookUp)
        {
            var translation = translationLookUp.Translate(from, to, originalTerm);
            return Create(from, to, originalTerm, translation);
        }
        public static Translation Create(Iso from, Iso to, string originalTerm, string translatedTerm) =>
            IsValidTranslation(from, to, originalTerm, translatedTerm)
            ? new Translation(from, to, originalTerm, translatedTerm)
            : Translation.Empty;
        protected override IEnumerable<object> GetEqualityComponents() =>
            new object[]
            {
                From,
                To,
                OriginalTerm,
                TranslatedTerm
            };

        private static bool IsValidTranslation(Iso from, Iso to, string originalTerm, string translatedTerm) =>
            from != Iso.Empty &&
            to != Iso.Empty &&
            !string.IsNullOrWhiteSpace(originalTerm) &&
            !string.IsNullOrWhiteSpace(translatedTerm);


    }
}
