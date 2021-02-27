using EasyLanguageLearning.API.ViewModels;
using EasyLanguageLearning.Application;
using EasyLanguageLearning.Application.Evaluations;
using EasyLanguageLearning.Domain.Evaluations;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.VocabularyUnits;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLanguageLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IEvaluationsService evaluationsService;

        public ExercisesController(IEvaluationsService evaluationsService)
        {
            this.evaluationsService = evaluationsService;
        }


        [HttpGet("Writing")]
        public async Task<IEnumerable<WritingExerciseViewModel>> GetWritingExercises(Guid lessonId)
        {
            var curLessonId = new LessonId(lessonId);
            var writingExercises = await evaluationsService.GetWritingExercisesBy(curLessonId);
            return writingExercises.Select(exr => new WritingExerciseViewModel { Heading = exr.Heading, Id = exr.Id.AsGuid() });
        }

        [HttpPost("Writing")]
        public async Task<ExerciseOutcomeDTO> EvaluateWriting([FromBody] WritingExerciseAnswerViewModel writtingAnswer)
        {
            var result = await evaluationsService.EvaluateAnswer(new WritingExerciseId(writtingAnswer.Id), writtingAnswer.Answer);
            return result;
        }
    }
}
