using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.VocabularyUnits
{
    public class VocabularyUnitRepository : IVocabularyUnitRepository
    {
        private DataContext dataContext;

        public VocabularyUnitRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public Task<VocabularyUnit> GetBy(LessonId lessonId) =>
            Task.Factory.StartNew(() => {
                //var unitList = dataContext.VocabularyUnits.Include(x => x.VocabularyItems).ToList();
                var unitList = dataContext.VocabularyUnits.ToList();
                return unitList.FirstOrDefault(vu => vu.LessonId == lessonId);
                });

    }
}
