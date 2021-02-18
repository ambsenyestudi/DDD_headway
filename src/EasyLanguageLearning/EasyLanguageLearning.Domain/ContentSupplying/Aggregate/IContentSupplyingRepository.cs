using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.ContentSupplying.Aggregate
{
    public interface IContentSupplyingRepository
    {
       Task<LearningPath> GetLearningPath(Iso motherIso, Iso learningIso);
    }
}
