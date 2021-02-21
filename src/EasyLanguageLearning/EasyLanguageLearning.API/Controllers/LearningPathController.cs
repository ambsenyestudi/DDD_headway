using EasyLanguageLearning.API.ViewModels;
using EasyLanguageLearning.Application.LearningPaths;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningPathController : ControllerBase
    {
        private readonly ILearningPathService learningPathService;

        public LearningPathController(ILearningPathService learningPathService)
        {
            this.learningPathService = learningPathService;
        }
        [HttpGet]
        public async Task<IEnumerable<LearningPathViewModel>> Get(string iso)
        {
            if (!Enum.TryParse<IsoCodes>(iso, out IsoCodes isoMother))
            {
                throw new ArgumentException("Invalid iso code");
            }

            var paths = await learningPathService.ListPathsForIso(isoMother);
            var pathVMs = paths
                .Select(cou =>
                    new LearningPathViewModel
                    {
                        Id = cou.Id.AsGuid(),
                        Name = cou.Name
                    });
            return pathVMs;
        }
    }
}
