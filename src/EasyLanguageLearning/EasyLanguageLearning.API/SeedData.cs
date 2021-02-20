using EasyLanguageLearning.Infrastructure;
using EasyLanguageLearning.Infrastructure.ContentSupplying;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EasyLanguageLearning.API
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider services)
        {
            using (var dbContext = new DataContext(
               services.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                
                //PopulateCoursesData(dbContext);
                PopulateLearningPaths(dbContext);

            }
        }
        /*
        public static void PopulateCoursesData(DataContext dbContext)
        {
            
            if (dbContext.Courses.Any())
            {
                return;   // DB has been seeded
            }
            foreach (var item in dbContext.Courses)
            {
                dbContext.Remove(item);
            }

            dbContext.SaveChanges();
            dbContext.Courses.Add(new CourseDTO
            {
                Id = new Guid("c667c917-6857-437d-b05c-cb10d055cffe"),
                Name = "French 1",
                MotherLanguage = "English",
                LearningLanguage = "French",
                MotherLanguageIso = "en",
                LearningLanguageIso = "fr"
            });
            dbContext.Courses.Add(new CourseDTO
            {
                Id = new Guid("3759cfa7-b989-4519-a763-b5f695916429"),
                Name = "Spanish 1",
                MotherLanguage = "English",
                LearningLanguage = "Spanish",
                MotherLanguageIso = "en",
                LearningLanguageIso = "es"
            });

            dbContext.SaveChanges();
            
        }
        */
        public static void PopulateLearningPaths(DataContext dbContext) 
        {
            /*
            foreach (var item in dbContext.LearningPaths)
            {
                dbContext.Remove(item);
            }
            
            dbContext.SaveChanges();
            */

            var id = Guid.NewGuid();
            var path = new LearningPathDM { Id = id, Name = "French"};
            dbContext.LearningPaths.Add(path);
            dbContext.SaveChanges();
        }
    }
}
