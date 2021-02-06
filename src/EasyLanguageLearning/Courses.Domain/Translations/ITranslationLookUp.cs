using EasyLanguageLearning.Domain.Shared.Kernel.Languages;

namespace Courses.Domain.Translations
{
    public interface ITranslationLookUp
    {
        public string Translate(Iso from, Iso to, string locution);
    }
}
