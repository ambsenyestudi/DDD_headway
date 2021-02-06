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
            if(string.IsNullOrWhiteSpace(translation))
            {
                return Translation.Empty;
            }
            return new Translation(from, to, originalTerm, translation);
        }

        protected override IEnumerable<object> GetEqualityComponents() =>
            new object[]
            {
                From,
                To,
                OriginalTerm,
                TranslatedTerm
            };
    }
}
