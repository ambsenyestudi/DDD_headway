using EasyLanguageLearning.API.ViewModels;
using EasyLanguageLearning.Application.VocabularyUnits;
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
            var path = await contentSupplyingRepository.GetLearningPath(courseId);
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
            if(vocabularyUnit == null)
            {
                return new List<Vocabulary>();
            }
            return vocabularyUnit.VocabularyItems;
        }

        [HttpPost("Content")]
        public async Task<Guid> UpsertVocabulary([FromBody] VocabularyUnitDTO vocabularyUnit)
        {
            //todo invoKe service
            return Guid.Empty;
        }
    }
}
