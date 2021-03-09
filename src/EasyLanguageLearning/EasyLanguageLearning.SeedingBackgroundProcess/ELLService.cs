using EasyLanguageLearning.Application.LanguageCatalog;
using EasyLanguageLearning.Application.LearningPaths;
using EasyLanguageLearning.Application.VocabularyUnits;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using EasyLanguageLearning.SeedingBackgroundProcess.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.SeedingBackgroundProcess
{
    public class ELLService
    {
        private static int delay = 100;
        private readonly ELLGateway gateway;
        private readonly LearningPathStore learningPathStore;
        private readonly LanguageCatalogStore languageCatalogStore;
        private readonly VocabularyStore vocabularyStore;

        public ELLService(ELLGateway gateway, LearningPathStore learningPathStore, LanguageCatalogStore languageCatalogStore, VocabularyStore vocabularyStore)
        {
            this.gateway = gateway;
            this.learningPathStore = learningPathStore;
            this.languageCatalogStore = languageCatalogStore;
            this.vocabularyStore = vocabularyStore;
        }

        public async Task WaitForReady()
        {
            bool isReady = false;
            while (isReady == false)
            {
                try
                {
                    await gateway.GetReadyState();
                    isReady = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Not ready");
                    await Task.Delay(delay);
                    if (delay < 3000)
                    {
                        delay *= 2;
                    }

                }
            }
        }

        public async Task<bool> IsSeedingNeeded()
        {
            var isEmpty = await gateway.GetLearningPath();
            return isEmpty;
        }

        public async Task SeedLearningPaths()
        {
            var catalog = languageCatalogStore.Create();
            foreach (var currlang in catalog.Items)
            {
                var langDTO = ToLearningLanguageDTO(currlang, catalog.Iso);
                await gateway.PostCatalog(langDTO);
            }
            await InsertFrench(catalog);
            await InsertSpanish(catalog);
            await InsertGerman(catalog);

            await InsertFrentVocabularyLesson1();
            
        }

        private async Task InsertFrentVocabularyLesson1()
        {
            var vocUnit = vocabularyStore.CreateEnFr(vocabularyStore.GetEnFrLesson1());
            var unitDTO = ToVocabularyUnitDTO(vocUnit);
            await gateway.PostUnit(unitDTO);
        }

        private VocabularyUnitDTO ToVocabularyUnitDTO(VocabularyUnit vocUnit) =>
            new VocabularyUnitDTO
            {
                Id = vocUnit.Id.Value,
                LessonId = vocUnit.LessonId.Value,
                LearningIso = vocUnit.LearningLanguageIso.ToString(),
                MotherIso = vocUnit.MotherLanguageIso.ToString(),
                VocabularyList = ToTranslationDTO(vocUnit.ListItems())
            };

        private TranslationDTO[] ToTranslationDTO(IEnumerable<Vocabulary> vocabularyItems) =>
            vocabularyItems
                .Select(v => ToTranslationDTO(v))
                .ToArray();

        private TranslationDTO ToTranslationDTO(Vocabulary vocabulary) =>
            new TranslationDTO
            {
                Original = vocabulary.MotherLanguageTerm,
                Translation = vocabulary.LearningLanguageTerm
            };

        private async Task InsertFrench(LanguageCatalog catalog)
        {
            var frenchLang = learningPathStore.GetFrenchLang(catalog);
            var frenchLeaningPath = learningPathStore.CreateFrenchPath(frenchLang);

            var frencPathDTO = new LearningPathDTO
            {
                Id = frenchLeaningPath.Id.Value,
                LearningLanguage = ToLearningLanguageDTO(frenchLang, catalog.Iso),
                CourseList = ToCourseDTO(frenchLeaningPath.Courses)
            };

            await gateway.PostLeaningPath(frencPathDTO);
        }

        private async Task InsertSpanish(LanguageCatalog catalog)
        {
            var spanishLang = learningPathStore.GetSpanishLang(catalog);
            var spanishLeaningPath = learningPathStore.CreateSpanishPath(spanishLang);

            var spanishPathDTO = new LearningPathDTO
            {
                Id = spanishLeaningPath.Id.Value,
                LearningLanguage = ToLearningLanguageDTO(spanishLang, catalog.Iso),
                CourseList = ToCourseDTO(spanishLeaningPath.Courses)
            };

            await gateway.PostLeaningPath(spanishPathDTO);
        }

        private async Task InsertGerman(LanguageCatalog catalog)
        {
            var germanLang = learningPathStore.GetGermanLang(catalog);
            var germanLeaningPath = learningPathStore.CreateGermanPath(germanLang);

            var germanPathDTO = new LearningPathDTO
            {
                Id = germanLeaningPath.Id.Value,
                LearningLanguage = ToLearningLanguageDTO(germanLang, catalog.Iso),
                CourseList = ToCourseDTO(germanLeaningPath.Courses)
            };

            await gateway.PostLeaningPath(germanPathDTO);
        }

        private LearningLanguageDTO ToLearningLanguageDTO(LearningLanguage language, Iso motherIso) =>
            new LearningLanguageDTO
            {
                Id = language.Id.Value,
                Iso = language.Iso.ToString(),
                LanguageCatalogId = language.LanguageCatalogId.Value,
                MotherIso = motherIso.ToString(),
                Name = language.Name
            };
        private CourseDTO[] ToCourseDTO(ICollection<Course> courses) =>
            courses
                .Select(c => ToCourseDTO(c))
                .ToArray();
        private CourseDTO ToCourseDTO(Course course) =>
            new CourseDTO 
            { 
                Id = course.Id.Value,
                Level = course.Level.Value,
                LessonList = ToLessonDTO(course.Lessons)
            };

        private LessonDTO[] ToLessonDTO(ICollection<Lesson> lessons) =>
            lessons
                .Select(l => ToLessonDTO(l))
                .ToArray();

        private LessonDTO ToLessonDTO(Lesson lesson) =>
            new LessonDTO
            {
                Id = lesson.Id.Value,
                Level = lesson.Level.Value,
                Name = lesson.Name
            };
    }
}
