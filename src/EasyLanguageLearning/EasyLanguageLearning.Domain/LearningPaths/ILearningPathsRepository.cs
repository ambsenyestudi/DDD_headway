using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.ContentSupplying
{
    public interface ILearningPathsRepository
    {
        Task<LearningPath> GetLearningPath(Iso motherIso, Iso learningIso);
        Task<IEnumerable<LearningPath>> ListPathsForIso(Iso iso);
        Task<Guid> Upsert(LearningPath insertingPath);
        Task<LearningPath> GetLearningPath(Guid courseId);
    }
}
