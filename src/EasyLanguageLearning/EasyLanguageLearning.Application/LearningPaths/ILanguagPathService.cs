using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Application.LearningPaths
{
    public interface ILearningPathService
    {
        Task<LearningPath> GetPath(IsoCodes motherLanguageIso, IsoCodes learningLanguageIso);
        Task<IEnumerable<LearningPath>> ListPathsForIso(IsoCodes isoMother);
        Task<Guid> Insert(LearningPathDTO learningPath);
    }
}
