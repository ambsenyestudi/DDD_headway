using EasyLanguageLearning.API.ViewModels;
using EasyLanguageLearning.Domain.ContentSupplying.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLanguageLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IContentSupplyingRepository contentSupplyingRepository;

        public CoursesController(IContentSupplyingRepository contentSupplyingRepository)
        {
            this.contentSupplyingRepository = contentSupplyingRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<CourseViewModel>> Get()
        {
            var path = await contentSupplyingRepository.GetCourse(Iso.CreateIso(IsoCodes.en), Iso.CreateIso(IsoCodes.fr));
            return new List<CourseViewModel>
            {
                new CourseViewModel { Name = path.Name }
            };

        }
    }
}
