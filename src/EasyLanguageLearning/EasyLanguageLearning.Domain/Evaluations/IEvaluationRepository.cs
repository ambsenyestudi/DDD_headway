using EasyLanguageLearning.Domain.Evaluations.Aggregate;
using EasyLanguageLearning.Domain.VocabularyUnits;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.Evaluations
{
    public interface IEvaluationRepository
    {
        Task<WritingExercise> GetWritingExerciseBy(VocabularyId id);
        Task<WritingExercise> GetWritingExerciseBy(WritingExerciseId id);
    }
}
