using EasyLanguageLearning.Domain.Evaluations;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.Evaluations
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly DataContext dataContext;

        public EvaluationRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public Task<WritingExercise> GetWritingExerciseBy(VocabularyId id) =>
            Task.Factory.StartNew(() =>
                dataContext.WritingExercises.FirstOrDefault(vu => vu.VocabularyId == id));

        public Task<WritingExercise> GetWritingExerciseBy(WritingExerciseId id) =>
            Task.Factory.StartNew(() =>
                dataContext.WritingExercises.FirstOrDefault(vu => vu.Id == id));
    }
}
