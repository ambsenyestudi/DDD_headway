using System;
using System.Collections.Generic;
using System.Text;

namespace EasyLanguageLearning.Domain.ContentSupplying
{
    public class ContentSupplyingAggreate
    {
        public LearningPath CareteLearningPath(Guid id, string name)
        {
            var path = new LearningPath(id);
            path.UpdatName(name);
            return path;
        }
    }
}
