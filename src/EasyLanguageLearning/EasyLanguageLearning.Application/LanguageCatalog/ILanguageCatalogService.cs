using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Application.LanguageCatalog
{
    public interface ILanguageCatalogService
    {
        Task<Guid> InsertLanguage(LearningLanguageDTO learningLanguage);
    }
}
