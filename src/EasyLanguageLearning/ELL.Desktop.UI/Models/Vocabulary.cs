using System;

namespace ELL.Desktop.UI.Models
{
    public class Vocabulary
    {
        public Guid Id { get; set; }
        public string MotherLanguageTerm { get; set; }
        public string LearningLanguageTerm { get; set; }
        public string TranslatedContent { get => $"{MotherLanguageTerm}: {LearningLanguageTerm}"; }
    }
}
