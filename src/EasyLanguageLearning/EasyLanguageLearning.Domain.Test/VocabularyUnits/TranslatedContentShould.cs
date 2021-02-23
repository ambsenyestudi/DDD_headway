using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits;
using System.Collections.Generic;
using Xunit;

namespace EasyLanguageLearning.Domain.Test.VocabularyUnits
{
    public class TranslatedContentShould
    {
        [Fact]
        public void CreateContentFromDifferentLanguages()
        {
            var content = TranslatedContent.Create(
                Iso.CreateIso(IsoCodes.en),
                "Yes",
                Iso.CreateIso(IsoCodes.fr),
                "Oui");
            Assert.NotEqual(TranslatedContent.Empty, content);
        }

        [Fact]
        public void NotAllowSameIso()
        {
            var content = TranslatedContent.Create(
                Iso.CreateIso(IsoCodes.fr),
                "Oui",
                Iso.CreateIso(IsoCodes.fr),
                "Oui");
            Assert.Equal(TranslatedContent.Empty, content);
        }

        [Fact]
        public void TellRepetitionsInSet()
        {
            var previousContentList = new List<TranslatedContent>
            {
                TranslatedContent.Create(
                    Iso.CreateIso(IsoCodes.en),
                    "Yes",
                    Iso.CreateIso(IsoCodes.fr),
                    "Oui")
            };
            var newContent = TranslatedContent.Create(
                Iso.CreateIso(IsoCodes.en),
                "Yes",
                Iso.CreateIso(IsoCodes.fr),
                "Oui");
            Assert.True(newContent.IsIn(previousContentList));
        }
    }
}
