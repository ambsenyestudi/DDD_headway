using EasyLanguageLearning.Application.LanguageCatalog;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyLanguageLearning.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ILanguageCatalogService service;

        public CatalogController(ILanguageCatalogService service)
        {
            this.service = service;
        }
        
        [HttpPost]
        public async Task InsertLanguageInCatalog([FromBody] LearningLanguageDTO language)
        {
            await service.InsertLanguage(language);
        }

    }
}
