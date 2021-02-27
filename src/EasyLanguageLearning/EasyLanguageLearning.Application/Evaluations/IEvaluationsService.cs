using EasyLanguageLearning.Domain.Evaluations;
using EasyLanguageLearning.Domain.Evaluations.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Application.Evaluations
{
    public interface IEvaluationsService
    {
        Task<IList<WritingExercise>> GetWritingExercisesBy(LessonId lesson);
        Task<ExerciseOutcomeDTO> EvaluateAnswer(WritingExerciseId writingExerciseId, string answer);
    }
}
