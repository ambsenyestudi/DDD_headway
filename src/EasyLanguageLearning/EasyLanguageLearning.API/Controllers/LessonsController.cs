using EasyLanguageLearning.API.ViewModels;
using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using EasyLanguageLearning.Domain.VocabularyUnits;
using EasyLanguageLearning.Domain.VocabularyUnits.Aggregate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILearningPathsRepository contentSupplyingRepository;
        private readonly IVocabularyUnitRepository vocabularyUnitRepository;

        public LessonsController(
            ILearningPathsRepository contentSupplyingRepository, 
            IVocabularyUnitRepository vocabularyUnitRepository)
        {
            this.contentSupplyingRepository = contentSupplyingRepository;
            this.vocabularyUnitRepository = vocabularyUnitRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<LessonViewModel>> Get(Guid courseId)
        {
            var path = await contentSupplyingRepository.GetLearningPath(Iso.CreateIso(IsoCodes.en), Iso.CreateIso(IsoCodes.fr));
            var course = path.Courses.FirstOrDefault(c => c.Id.AsGuid() == courseId);
            if(course!=null)
            {
                var lessons = course.Lessons.Select(le =>
                    new LessonViewModel 
                    { 
                        Id = le.Id.AsGuid(),
                        Name = le.Name
                    });
                return lessons;
            }


            throw new ArgumentException("Ooops no courses");
        }

        [HttpGet("Content")]
        public async Task<IEnumerable<Vocabulary>> GetContent(Guid lessonId)
        {
            //todo get vocabulary unit Id from lesson
            var vocabularyUnit = await vocabularyUnitRepository.GetBy(new LessonId(lessonId));
            return vocabularyUnit.VocabularyItems;
        }
        [HttpGet("Writing")]
        public async Task<IEnumerable<WritingExercise>> GetWritingExercises(Guid lessonId)
        {
            //todo get vocabulary unit Id from lesson
            var vocabularyUnit = await vocabularyUnitRepository.GetBy(new LessonId(lessonId));
            var list = new List<WritingExercise>();
            foreach( var voc in vocabularyUnit.VocabularyItems)
            {
                var writingExercise = await vocabularyUnitRepository.GetWritingExerciseBy(voc.Id);
                list.Add(writingExercise);
            }
            return list;
        }
    }
}
