using EasyLanguageLearning.Application.LearningPaths;
using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.LearningPaths
{
    public class LearningPathService : ILearningPathService
    {
        private readonly ILearningPathsRepository repository;
        private readonly ILanguageCatalogRepository catalogRepository;

        public LearningPathService(ILearningPathsRepository repository, ILanguageCatalogRepository catalogRepository)
        {
            this.repository = repository;
            this.catalogRepository = catalogRepository;
        }
        public Task<LearningPath> GetPath(IsoCodes motherLanguageIso, IsoCodes learningLanguageIso) =>
            repository.GetLearningPath(
                Iso.CreateIso(motherLanguageIso), 
                Iso.CreateIso(learningLanguageIso));

        public Task<IEnumerable<LearningPath>> ListPathsForIso(IsoCodes isoMother) =>
            repository.ListPathsForIso(Iso.CreateIso(isoMother));

        public async Task<Guid> Insert(LearningPathDTO learningPath)
        {
            if(!Iso.TryParse(learningPath.LearningLanguage.MotherIso, out Iso motherIso) ||
                !Iso.TryParse(learningPath.LearningLanguage.Iso, out Iso learningIso))
            {
                return Guid.Empty;
            }
            //todo imporve this waste of resources
            var paths = await repository.ListPathsForIso(motherIso);
            var currPath = paths.FirstOrDefault(p => p.MotherLanguageIso == motherIso);
            //impover this mess

            var insertingPath = await CreateFromDTO(learningPath, motherIso, learningIso);
            var courseIdList = insertingPath.Courses.Select(x => x.Id);
            if (currPath != null && currPath.Courses.All(c => courseIdList.Contains(c.Id)))
            {
                return Guid.Empty;
            }

            return await repository.Upsert(insertingPath);
        }

        private async Task<LearningLanguage> GetLanguage(Iso motherIso, Iso learningIso)
        {
            var catalog = await catalogRepository.GetBy(motherIso);
            return catalog.Items.FirstOrDefault(x => x.Iso == learningIso);
        }

        private async Task<LearningPath> CreateFromDTO(LearningPathDTO learningPath, Iso motherIso, Iso learningIso)
        {
            var learningLanguage = await GetLanguage(motherIso, learningIso);
            var path = new LearningPath(learningPath.Id, learningLanguage, motherIso);
            foreach (var course in learningPath.CourseList)
            {
                var couresId = path.AddCourseFromLevel(course.Level, course.Id.ToString());
                foreach (var lesson in course.LessonList)
                {
                    path.AddLessonToCourse(couresId, lesson.Name, lesson.Level, lesson.Id.ToString());
                }

            }
            return path;
        }
    }
}
