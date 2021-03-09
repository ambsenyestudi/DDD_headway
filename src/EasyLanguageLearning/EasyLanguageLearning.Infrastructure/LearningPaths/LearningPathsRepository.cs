using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.Infrastructure.ContentSupplying
{

    public class LearningPathsRepository : ILearningPathsRepository
    {
        private readonly DataContext context;
        private readonly IVocabularyUnitRepository vocabularyRepository;

        public LearningPathsRepository(DataContext context,
            IVocabularyUnitRepository vocabularyRepository)
        {
            this.context = context;
            this.vocabularyRepository = vocabularyRepository;
        }

        public Task<LearningPath> GetLearningPath(Iso motherIso, Iso learningIso) =>
            Task.Factory.StartNew(() => 
                context.LearningPaths
                    .FirstOrDefault(x => x.LearningLanguageIso == learningIso));

        

        public Task<IEnumerable<LearningPath>> ListPathsForIso(Iso iso) =>
            Task.Factory.StartNew(()=>
                context.LearningPaths
                    .Where(x => x.MotherLanguageIso == iso)
                    .AsEnumerable()
                );

        public async Task<Guid> Upsert(LearningPath updatingPath)
        {
            var path = context.LearningPaths.FirstOrDefault(lp => lp.Id == updatingPath.Id);
            if(path == null)
            {
                var newPathId = await Insert(updatingPath);
                foreach (var course in updatingPath.Courses)
                {
                    foreach (var lesson in course.Lessons)
                    {
                        var id = Guid.NewGuid();
                        var vocUnit = new VocabularyUnit(id, lesson.Id, updatingPath.MotherLanguageIso, updatingPath.LearningLanguageIso);
                        var vocUnitId = vocabularyRepository.Insert(vocUnit);
                    }
                }
                return newPathId;
            }
            var existingId = await EnsurePathIsThere(updatingPath.MotherLanguageIso,updatingPath.LearningLanguageIso);
            if(existingId != Guid.Empty)
            {
                return existingId;
            }
            context.Update<LearningPath>(updatingPath);
            context.SaveChanges();
            var learningPathId = await EnsurePathIsThere(updatingPath.MotherLanguageIso, updatingPath.LearningLanguageIso);
            return learningPathId;
        }

        private async Task<Guid> Insert(LearningPath insertingPath)
        {
            
            context.LearningPaths.Add(insertingPath);
            context.SaveChanges();
            var id = await EnsurePathIsThere(insertingPath.MotherLanguageIso, insertingPath.LearningLanguageIso);
            return id;
        }

        private async Task<Guid> EnsurePathIsThere(Iso motherIso, Iso learningIso)
        {
            var path = await GetLearningPath(motherIso, learningIso);
            if (path == null)
            {
                return Guid.Empty;
            }
            return path.Id.Value;
        }

        public Task<LearningPath> GetLearningPath(Guid courseId) =>
            Task.Factory.StartNew(() => 
                context.LearningPaths
                    .FirstOrDefault(x => x.Courses.Any(c=>c.Id == new CourseId(courseId))));
    }
}
