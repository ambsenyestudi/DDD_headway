using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
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

        public Task<LanguageCatalog> GetBy(LanguageCatalogId id) => 
            Task.Factory.StartNew(() =>
                 context.LanguageCatalogs.FirstOrDefault(lc => lc.Id == id));

        public async Task<LanguageCatalogId> Upsert(LanguageCatalog updatingCatalog)
        {
            var catalog = await GetBy(updatingCatalog.Id);
            if(catalog != null)
            {
                context.Update<LanguageCatalog>(updatingCatalog);
                await context.SaveChangesAsync();
                return updatingCatalog.Id;
            }
            return await InsertCatalog(updatingCatalog);
        }

        private async Task<LanguageCatalogId> InsertCatalog(LanguageCatalog updatingCatalog)
        {
            context.LanguageCatalogs.Add(updatingCatalog);
            await context.SaveChangesAsync();
            var catalog = await GetBy(updatingCatalog.Id);
            return catalog.Id;
        }
    }
}
