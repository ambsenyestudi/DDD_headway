using ELL.Desktop.UI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.VocabularyUnits
{
    public interface IVocabularyService
    {
        Task<List<Vocabulary>> GetVocabulary(Guid lessonId);
    }
}