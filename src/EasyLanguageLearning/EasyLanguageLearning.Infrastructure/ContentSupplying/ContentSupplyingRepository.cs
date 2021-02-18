using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.ContentSupplying.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.ContentSupplying
{

    public class ContentSupplyingRepository : IContentSupplyingRepository
    {
        private readonly DataContext context;
        public ContentSupplyingRepository(DataContext context)
        {
            this.context = context;
        }
        public Task<LearningPath> GetLearningPath(Iso motherIso, Iso learningIso)
        {
            //todo filter by iso
            var db = context.LearningPaths.First();
            return Task.FromResult((LearningPath)db);
        }
    }
}
