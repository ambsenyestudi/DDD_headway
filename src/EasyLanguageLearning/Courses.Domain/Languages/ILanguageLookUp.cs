using EasyLanguageLearning.Domain.Shared.Kernel.Languages;

namespace Courses.Domain.Languages
{
    public interface ILanguageLookUp
    {
        Language GetLanguage(Iso iso);
        bool CatalogContains(Iso lanaguagIso);
    }
}
