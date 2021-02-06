using System.Collections.Generic;
using System.Linq;

namespace Courses.Domain
{
    public class LanguageCatalog
    {
        private readonly IEnumerable<Iso> languages;
        public LanguageCatalog(IEnumerable<Iso> languages)
        {
            this.languages = languages;
        }
        public bool Contains(params Iso[] isoList) =>
            isoList.All(iso => languages.Contains(iso));
    }
}
