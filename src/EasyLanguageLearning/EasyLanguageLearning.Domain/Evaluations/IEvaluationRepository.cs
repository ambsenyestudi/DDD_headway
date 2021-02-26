using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.Evaluations
{
    public interface IEvaluationRepository
    {
        Task<WritingExercise> GetWritingExerciseBy(VocabularyId id);
        Task<WritingExercise> GetWritingExerciseBy(WritingExerciseId id);
    }
}
