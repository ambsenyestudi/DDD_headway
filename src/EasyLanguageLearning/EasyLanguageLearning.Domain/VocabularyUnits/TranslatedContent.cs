using EasyLanguageLearning.Domain.Shared.Kernel;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System.Collections.Generic;

namespace EasyLanguageLearning.Domain.VocabularyUnits
{
    public class TranslatedContent : ValueObject
    {
        public static TranslatedContent Empty { get; } = new TranslatedContent(Iso.Empty, string.Empty, Iso.Empty, string.Empty);
        public Iso MotherLanguageIso { get; }
        public Iso LearningLanguageIso { get; }
        public string MotherLanguageTerm { get; }
        public string LearningLanguageTerm { get; }
        

        private TranslatedContent(
            Iso motherLanguageIso,
            string motherLanguageTerm,
            Iso learningLanguageIso,
            string learningLanguageTerm)
        {
            MotherLanguageIso = motherLanguageIso;
            LearningLanguageIso = learningLanguageIso;
            MotherLanguageTerm = motherLanguageTerm;
            LearningLanguageTerm = learningLanguageTerm;
        }

        public static TranslatedContent Create(
            Iso motherLanguageIso,
            string motherLanguageTerm,
            Iso learningLanguageIso,
            string learningLanguageTerm)
        {
            if(motherLanguageIso == learningLanguageIso)
            {
                return Empty;
            }
            return new TranslatedContent(
                motherLanguageIso, 
                motherLanguageTerm, 
                learningLanguageIso, 
                learningLanguageTerm);

        }

        public bool IsIn(List<TranslatedContent> previousContentList) =>
            previousContentList.Contains(this);

        protected override IEnumerable<object> GetEqualityComponents() =>
            new object[] { 
                MotherLanguageIso, 
                MotherLanguageTerm, 
                LearningLanguageIso, 
                LearningLanguageTerm };
    }
}
