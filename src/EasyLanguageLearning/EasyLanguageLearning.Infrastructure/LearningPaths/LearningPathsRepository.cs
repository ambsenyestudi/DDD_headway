using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.ContentSupplying
{

    public class LearningPathsRepository : ILearningPathsRepository
    {
        private readonly DataContext context;
        public LearningPathsRepository(DataContext context)
        {
            this.context = context;
        }

        public Task<LearningPath> GetLearningPath(Iso motherIso, Iso learningIso) =>
            Task.Factory.StartNew(() => 
                context.LearningPaths
                    .FirstOrDefault(x => x.LearningLanguageIso == learningIso));


        public Task<IEnumerable<LearningPath>> ListPathsForIso(Iso iso) =>
            Task.Factory.StartNew(()=>
                context.LearningPaths
                    .Where(x => x.MotherLanguageIso == iso)
                    .AsEnumerable()
                );
        
    }
}
