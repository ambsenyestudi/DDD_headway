using EasyLanguageLearning.Domain.LanguageContents;
using EasyLanguageLearning.Domain.LearningPaths;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.LanguageContents
{
    public class LanguageContentRepository : ILanguageContentRepository
    {
        private DataContext dataContext;

        public LanguageContentRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public Task<IEnumerable<LanguageContent>> GetBy(LessonId lessonId) => 
            Task.Factory.StartNew(() =>
                dataContext.LanguageContents.Where(lc => lc.LessonId == lessonId).AsEnumerable()
            );
    }
}
