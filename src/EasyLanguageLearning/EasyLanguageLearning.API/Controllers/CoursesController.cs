using EasyLanguageLearning.API.ViewModels;
using EasyLanguageLearning.Domain.ContentSupplying.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ILearningPathsRepository contentSupplyingRepository;

        public CoursesController(ILearningPathsRepository contentSupplyingRepository)
        {
            this.contentSupplyingRepository = contentSupplyingRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<CourseViewModel>> Get()
        {
            var path = await contentSupplyingRepository.GetLearningPath(Iso.CreateIso(IsoCodes.en), Iso.CreateIso(IsoCodes.fr));
            var course = path.Courses.First();
            return new List<CourseViewModel>
            {
                new CourseViewModel { Name = course.GetName().ToString() }
            };

        }
    }
}
