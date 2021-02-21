using EasyLanguageLearning.Domain.LearningPaths;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.LanguageContents
{
    public interface ILanguageContentRepository
    {
        Task<IEnumerable<LanguageContent>> GetBy(LessonId lessonId);
    }
}
