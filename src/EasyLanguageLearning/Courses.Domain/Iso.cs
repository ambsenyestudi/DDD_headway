using EasyLanguageLearning.Domain.Shared.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Domain
{
    public enum IsoCodes
    {
        None, en, es, fr, de, ca
    }
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
