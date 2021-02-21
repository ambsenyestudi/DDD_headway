using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.LanguageCatalogs
{
    public class LanguageCatalogRepository : ILanguageCatalogRepository
    {
        private readonly DataContext context;

        public LanguageCatalogRepository(DataContext context)
        {
            this.context = context;
        }
        public Task<LanguageCatalog> GetBy(Iso iso) => 
            Task.Factory.StartNew(()=>
                context.LanguageCatalogs.FirstOrDefault(lc => lc.Iso == iso));
        
    }
}
