using EasyLanguageLearning.Domain.Shared.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Domain.VocabularyUnits
{
    public class VocabularyId : ValueId
    {
        public VocabularyId(Guid id) : base(id)
        {
        }
    }
}
