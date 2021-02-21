using EasyLanguageLearning.Application.LearningPaths;
using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.LearningPaths
{
    public class LearningPathService : ILearningPathService
    {
        private readonly ILearningPathsRepository repository;

        public LearningPathService(ILearningPathsRepository repository)
        {
            this.repository = repository;
        }
        public Task<LearningPath> GetPath(IsoCodes motherLanguageIso, IsoCodes learningLanguageIso) =>
            repository.GetLearningPath(
                Iso.CreateIso(motherLanguageIso), 
                Iso.CreateIso(learningLanguageIso));

        public Task<IEnumerable<LearningPath>> ListPathsForIso(IsoCodes isoMother) =>
            repository.ListPathsForIso(Iso.CreateIso(isoMother));
    }
}
