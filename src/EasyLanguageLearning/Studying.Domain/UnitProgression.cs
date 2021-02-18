using Courses.Domain;
using EasyLanguageLearning.Domain.Shared.Kernel.Units;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Studying.Domain
{
    internal class UnitProgression
    {
        public UnitId Unit { get; }

        private readonly Dictionary<UnitContentItemId, bool> contentCompletionDictionary;

        public UnitProgression(UnitId unitId, List<UnitContentItemId> unitContentList)
        {
            Unit = unitId;
            contentCompletionDictionary = unitContentList.ToDictionary(u => u, u => false);
        }

        internal float GetCompletionPercentaje()
        {
            float count = contentCompletionDictionary.Values.Where(uc => uc).Count();
            return count / (float)contentCompletionDictionary.Count * 100f;
        }
    }
}