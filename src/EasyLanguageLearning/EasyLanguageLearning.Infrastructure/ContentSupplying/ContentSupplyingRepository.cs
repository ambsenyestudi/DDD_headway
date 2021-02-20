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

        public Task<Course> GetCourse(Iso motherIso, Iso learningIso)
        {
            return Task.FromResult(context.Courses.First());
        }

        public Task<LearningPath> GetLearningPath(Iso motherIso, Iso learningIso)
        {
            return Task.FromResult(context.LearningPaths.First());
        }
    }
}
