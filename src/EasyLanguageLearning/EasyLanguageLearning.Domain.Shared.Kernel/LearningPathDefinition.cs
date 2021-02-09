using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.Shared.Kernel
{
    public class LearningPathDefinition : ValueObject
    {
        public static LearningPathDefinition Empty { get; } = new LearningPathDefinition(Iso.Empty, Iso.Empty);
        public Iso MotherLanguageIso { get; private set; }
        public Iso LearningLanguageIso { get; private set; }
        protected LearningPathDefinition(Iso motherLanaguageIso, Iso learningLanaguageIso)
        {
            MotherLanguageIso = motherLanaguageIso;
            LearningLanguageIso = learningLanaguageIso;
        }

        protected override IEnumerable<object> GetEqualityComponents() =>
            new object[] { MotherLanguageIso, LearningLanguageIso };

        public static LearningPathDefinition Create(Iso motherLanaguageIso, Iso learningLanaguageIso)
        {
            if(motherLanaguageIso == Iso.Empty || 
                learningLanaguageIso == Iso.Empty ||
                motherLanaguageIso == learningLanaguageIso)
            {
                return Empty;
            }
            return new LearningPathDefinition(Iso.Empty, Iso.Empty);
        }
    }
}
