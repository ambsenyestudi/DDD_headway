using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.VocabularyUnits
{
    public interface IVocabularyUnitRepository
    {
        Task<VocabularyUnit> GetBy(LessonId id);
        Guid Insert(VocabularyUnit vocUnit);
    }
}
