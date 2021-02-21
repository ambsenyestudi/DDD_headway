using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace EasyLanguageLearning.Infrastructure
{
    public class DataContext : DbContext
    {

        public DbSet<LearningPath> LearningPaths { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            BuildLearningPathModel(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }
        private void BuildLearningPathModel(ModelBuilder modelBuilder)
        {
            var learningPathBuilder = modelBuilder.Entity<LearningPath>();
            learningPathBuilder.ToTable(nameof(LearningPaths));
            learningPathBuilder.Property(x => x.Id).HasConversion(x => x.Value, v => new LearningPathId(v)).HasColumnName(nameof(LearningPath.Id)).IsRequired();
            learningPathBuilder.OwnsMany(
                p => p.Courses,
                cou => {
                    cou.Property(ca => ca.Level)
                    .HasConversion(
                        coNa => coNa.Value, 
                        level => Level.Create(level));
                    cou.Property(ca => ca.LearningPathId)
                        .HasConversion(lpId => lpId.Value, lpGuid => new LearningPathId(lpGuid))
                        .HasColumnName(nameof(Course.LearningPathId)).IsRequired();
                    cou.WithOwner().HasConstraintName(nameof(Course.LearningPathId));
                    cou.Property(ca => ca.Id)
                        .HasConversion(coId => coId.Value, coGiod => new CourseId(coGiod))
                        .HasColumnName(nameof(Course.Id)).IsRequired();
                    cou.HasKey(e => e.Id);
                });
            modelBuilder.Entity<LearningPath>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
        
                
    }
}
