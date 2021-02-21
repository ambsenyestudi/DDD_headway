using EasyLanguageLearning.API.ViewModels;
using EasyLanguageLearning.Application.LearningPaths;
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
    public class CoursesController : ControllerBase
    {
        private readonly ILearningPathService learningPathService;

        public CoursesController(ILearningPathService learningPathService)
        {
            this.learningPathService = learningPathService;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseViewModel>> Get(string motherLanguageIso, string learningLanguageIso)
        {
            if(!Enum.TryParse<IsoCodes>(motherLanguageIso, out IsoCodes isoMother) ||
                !Enum.TryParse<IsoCodes>(learningLanguageIso, out IsoCodes isoLearning))
            {
                throw new ArgumentException("Invalid iso code");
            }
            
            var path = await learningPathService.GetPath(isoMother, isoLearning);
            var courses = path.Courses
                .Select(cou => 
                    new CourseViewModel 
                    { 
                        Id = cou.Id.AsGuid(),
                        Name = cou.GetName().ToString() 
                    });
            return courses;
        }
    }
}
