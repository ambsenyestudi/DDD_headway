using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public Task<VocabularyUnit> GetBy(VocabularyUnitId id) =>
            Task.Factory.StartNew(() =>
                dataContext.VocabularyUnits.FirstOrDefault(vu => vu.Id == id));
    }
}
