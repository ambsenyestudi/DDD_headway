using EasyLanguageLearning.API.ViewModels;
using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
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

        public LessonsController(ILearningPathsRepository contentSupplyingRepository)
        {
            this.contentSupplyingRepository = contentSupplyingRepository;
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
    }
}
