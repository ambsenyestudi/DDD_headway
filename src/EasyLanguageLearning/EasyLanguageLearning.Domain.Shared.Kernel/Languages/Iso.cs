using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.Shared.Kernel.Languages
{
    public class Iso : ValueObject
    {
        public static Iso Empty { get; } = new Iso("");
        public string IsoCode { get; }
        public Iso(string iso)
        {
            if (string.IsNullOrWhiteSpace(iso))
            {
                iso = string.Empty;
            }
            this.IsoCode = iso;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return (IsoCode);
        }

        public static Iso CreateIso(IsoCodes isoCode)
        {
            string iso = isoCode == IsoCodes.None
                ? string.Empty
                : isoCode.ToString();
            return new Iso(iso);
        }
    }
}
