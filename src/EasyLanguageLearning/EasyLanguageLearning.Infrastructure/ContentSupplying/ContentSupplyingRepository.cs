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
        private readonly ContentSupplyingAggreate aggreate;
        public ContentSupplyingRepository(DataContext context)
        {
            this.context = context;
            aggreate = new ContentSupplyingAggreate();
        }
        public Task<LearningPath> GetLearningPath(Iso motherIso, Iso learningIso)
        {
            //todo filter by iso
            var db = context.LearningPaths.First();
            var path = aggreate.CareteLearningPath(db.Id, db.Name);
            return Task.FromResult(path);
        }
    }
}
