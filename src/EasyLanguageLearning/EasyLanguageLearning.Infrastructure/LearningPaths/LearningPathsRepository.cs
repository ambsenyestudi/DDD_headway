using EasyLanguageLearning.Domain.ContentSupplying.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.ContentSupplying
{

    public class LearningPathsRepository : ILearningPathsRepository
    {
        private readonly DataContext context;
        public LearningPathsRepository(DataContext context)
        {
            this.context = context;
        }

        public Task<LearningPath> GetLearningPath(Iso motherIso, Iso learningIso)
        {
            return Task.FromResult(context.LearningPaths.First());
        }
    }
}
