using System;

namespace EasyLanguageLearning.Domain.LanguageContents
{
    public class LanguageContent
    {
        public Guid Id { get; protected set; }
        public TranslatedContent TranslatedContent { get; protected set; }
        //Todo map this in EF Core
        public LanguageContent()
        {

        }

        public LanguageContent(Guid id, TranslatedContent translatedContent)
        {
            Id = id;
            TranslatedContent = translatedContent;
        }

    }
}
