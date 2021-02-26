using EasyLanguageLearning.Domain.Evaluations;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Application.Evaluations
{
    public class EvaluationsService : IEvaluationsService
    {
        private readonly IVocabularyUnitRepository exerciseRepository;
        private readonly IEvaluationRepository evaluationRepository;

        public EvaluationsService(IVocabularyUnitRepository exerciseRepository, IEvaluationRepository evaluationRepository)
        {
            this.exerciseRepository = exerciseRepository;
            this.evaluationRepository = evaluationRepository;
        }

        public async Task<string> EvaluateAnswer(WritingExerciseId writingExerciseId, string answer)
        {
            var result = await Task.Factory.StartNew(() => "Good job");
            return result;
        }

        public async Task<IList<WritingExercise>> GetWritingExercisesBy(LessonId lessonId)
        {
            var list = new List<WritingExercise>();
            var vocabularyUnit = await exerciseRepository.GetBy(lessonId);

            if( vocabularyUnit==null || !vocabularyUnit.VocabularyItems.Any())
            {
                return list;
            }

            
            foreach (var voc in vocabularyUnit.VocabularyItems)
            {
                var writingExercise = await evaluationRepository.GetWritingExerciseBy(voc.Id);
                list.Add(writingExercise);
            }

            return list;
        }
    }
}
