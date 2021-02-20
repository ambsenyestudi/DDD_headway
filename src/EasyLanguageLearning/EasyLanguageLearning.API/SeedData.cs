using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.ContentSupplying.Aggregate;
using EasyLanguageLearning.Infrastructure;
using EasyLanguageLearning.Infrastructure.ContentSupplying;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
                var aggregate = new ContentSupplyingAggreate();
                if (!dbContext.LearningPaths.Any())
                {
                    PopulateLearningPaths(dbContext, aggregate);
                }
                if (!dbContext.Courses.Any())
                {
                    PopulateCourses(dbContext, aggregate);
                }
            }
        }
        public static void PopulateLearningPaths(DataContext dbContext, ContentSupplyingAggreate aggregate)
        {

            foreach (var item in dbContext.LearningPaths)
            {
                dbContext.Remove(item);
            }

            dbContext.SaveChanges();
            var listPathNameDictionary = new Dictionary<string, string>
            {
                ["French"] = "82d83571-5fdd-40c0-ac46-0eea57a19ab0"
            };
            foreach (var pathNameId in listPathNameDictionary)
            {
                dbContext.LearningPaths.Add(FromAggreage(aggregate, pathNameId.Key, pathNameId.Value));
            }

            dbContext.SaveChanges();
        }

        public static void PopulateCourses(DataContext dbContext, ContentSupplyingAggreate aggregate)
        {
            
            foreach (var item in dbContext.Courses)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();
            var pathCollection = dbContext.LearningPaths;
            foreach (var path in pathCollection)
            {
                dbContext.Courses.Add(FromAggregate(aggregate, path));
            }

            dbContext.SaveChanges();
        }
        
        public static LearningPath FromAggreage(ContentSupplyingAggreate aggregate, string name, string guid="")
        {
            var id = string.IsNullOrWhiteSpace(guid)
                ? Guid.NewGuid()
                : new Guid(guid);
            
            return aggregate.CareteLearningPath(id, name);
        }
        public static Course FromAggregate(ContentSupplyingAggreate aggregate, LearningPath learningPath, string guid="")
        {
            var id = string.IsNullOrWhiteSpace(guid)
                ? Guid.NewGuid()
                : new Guid(guid);

            return aggregate.CreateCourseFromPath(id, learningPath);
        }
}
}
