using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.VocabularyUnits
{
    public interface IVocabularyUnitRepository
    {
        Task<VocabularyUnit> GetBy(VocabularyUnitId id);
    }
}
