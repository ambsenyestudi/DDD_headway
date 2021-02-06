using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Domain
{
    public class LanguageCatalog
    {        
        private readonly IList<Language> languageList;
        public LanguageCatalog(IList<Language> languageList)
        {
            this.languageList = languageList;
        }
        public bool Contains(params Iso[] isoList) =>
            isoList.All(iso => languageList.Any(la => la.Iso == iso));
    }
}
