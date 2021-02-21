using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;

namespace EasyLanguageLearning.Domain.LanguageContents
{
    public class LanguageContent
    {
        public Guid Id { get; protected set; }
        public LessonId LessonId { get; protected set; }
        #region EF core
        public Iso MotherLanguageIso { get; protected set; }
        public Iso LearningLanguageIso { get; protected set; }
        public string MotherLanguageTerm { get; protected set; }
        public string LearningLanguageTerm { get; protected set; }
        
        public LanguageContent()
        {

        }
        #endregion EF core
        public LanguageContent(Guid id, LessonId lessonId, TranslatedContent translatedContent)
        {
            Id = id;
            LessonId = lessonId;
            MotherLanguageIso = translatedContent.MotherLanguageIso;
            LearningLanguageIso = translatedContent.LearningLanguageIso;
            MotherLanguageTerm = translatedContent.MotherLanguageTerm;
            LearningLanguageTerm = translatedContent.LearningLanguageTerm;
        }

    }
}
