using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.Shared.Kernel.Units
{
    public class UnitContentItem: ValueObject
    {
        public static UnitContentItem Empty { get; } = new UnitContentItem(string.Empty, string.Empty);
        public string MotherLangugeTerm { get; }
        public string LearningLanguageTerm { get; }
        protected UnitContentItem(string motherLangugeTerm, string learningLanguageTerm)
        {
            MotherLangugeTerm = motherLangugeTerm;
            LearningLanguageTerm = learningLanguageTerm;
        }

        protected override IEnumerable<object> GetEqualityComponents() =>
            new object[] { MotherLangugeTerm, LearningLanguageTerm };

        public static UnitContentItem Create(string motherLangugeTerm, string learningLanguageTerm)
        {
            if (string.IsNullOrWhiteSpace(motherLangugeTerm) ||
                  string.IsNullOrWhiteSpace(learningLanguageTerm))
            {
                return Empty;
            }
            return new UnitContentItem(motherLangugeTerm, learningLanguageTerm);
        }
    }
}
