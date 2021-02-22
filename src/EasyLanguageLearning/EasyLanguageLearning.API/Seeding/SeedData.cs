using EasyLanguageLearning.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EasyLanguageLearning.API.Seeding
{
    public class SeedData
    {
        
        
        
        
        public static void Initialize(IServiceProvider services)
        {
            using (var dbContext = new DataContext(
               services.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                if (!dbContext.LanguageCatalogs.Any())
                {
                    SeedLanguageCatalog.Populate(dbContext);
                }
                if (!dbContext.LearningPaths.Any())
                {
                    SeedLearningPath.Populate(dbContext);
                }
                if (!dbContext.VocabularyUnits.Any())
                {
                    SeedVocabulary.Populate(dbContext);
                }
                
            }
        }

        

        
        
    }
}
