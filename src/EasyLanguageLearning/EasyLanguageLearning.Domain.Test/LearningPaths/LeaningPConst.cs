using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Linq;

namespace EasyLanguageLearning.Domain.Test.LearningPaths
{
    public static class LeaningPConst
    {
        //"4860f206-3b4b-49da-8e4f-685fbe9240bf"
        //"7758807a-5015-47f5-b4fa-06c56b4dc400"
        public const string FR_LEARNING_PATH_NAME = "French";
        public const string FIRST_COURSE_ID = "2adf539c-e833-4fc8-b5d2-137686a39e92";
        public const string SECOND_COURSE_ID = "c023806e-82f4-42fd-81dc-3ba16e13a978";
        public const string LEARNING_PATH_ID = "990ea93e-b153-46db-8a3f-de9c59696b8c";
        public const string FIRST_LESSON_NAME = "Launch pad";

        public static Iso en_MotherIso { get; } = Iso.CreateIso(IsoCodes.en);
        public static LearningLanguage fr_LearningLanguage { get; } = CreateLearningLanguage(en_MotherIso, IsoCodes.fr);

        private static LearningLanguage CreateLearningLanguage(Iso motherIso, IsoCodes learnigLanguageIso)
        {
            var catalog = new LanguageCatalog(Guid.NewGuid(), motherIso);
            catalog.AddToCatalog(
                Iso.CreateIso(learnigLanguageIso),
                FR_LEARNING_PATH_NAME
                );
            return catalog.Items.First();
        }
    }
}
