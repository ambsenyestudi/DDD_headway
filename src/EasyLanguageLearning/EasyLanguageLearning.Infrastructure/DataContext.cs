using EasyLanguageLearning.Domain.ContentSupplying;
using EasyLanguageLearning.Domain.ContentSupplying.Aggregate;
using EasyLanguageLearning.Infrastructure.ContentSupplying;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace EasyLanguageLearning.Infrastructure
{
    public class DataContext : DbContext
    {

        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            BuildLearningPathModel(modelBuilder);
            BuildCourseModel(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }
        private void BuildLearningPathModel(ModelBuilder modelBuilder)
        {
            var learningPathBuilder = modelBuilder.Entity<LearningPath>();
            learningPathBuilder.ToTable(nameof(LearningPaths));
            learningPathBuilder.Property(x => x.Id).HasConversion(x => x.Value, v => new LearningPathId(v)).HasColumnName(nameof(LearningPath.Id)).IsRequired();
            
            modelBuilder.Entity<LearningPath>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
        
        
        private void BuildCourseModel(ModelBuilder modelBuilder)
        {
            var courseModelBuilder = modelBuilder.Entity<Course>();
            courseModelBuilder.ToTable(nameof(Courses));
            courseModelBuilder.Property(ca => ca.Id)
                .HasConversion(coId => coId.Value, coGiod => new CourseId(coGiod))
                .HasColumnName(nameof(Course.Id)).IsRequired();
            courseModelBuilder.Property(ca=>ca.LearningPathId)
                .HasConversion(lpId => lpId.Value, lpGuid => new LearningPathId(lpGuid))
                .HasColumnName(nameof(Course.LearningPathId)).IsRequired();

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
        
    }
}
