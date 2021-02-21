using EasyLanguageLearning.Domain.Shared.Kernel.Languages;

namespace EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate
{
    public interface ILanguaNameIsoService
    {
        string TransalteLanguageFromIso(Iso iso);
    }
}
