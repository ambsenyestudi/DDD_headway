using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.LanguageCatalogs
{
    public interface ILanguageCatalogRepository
    {
        Task<LanguageCatalog> GetBy(Iso iso);
    }
}
