using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

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
            learningPathBuilder.Property(x => x.Id)
                .HasConversion(x => x.Value, v => new LearningPathId(v))
                .HasColumnName(nameof(LearningPath.Id))
                .IsRequired();
            modelBuilder.Entity<LearningPath>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            learningPathBuilder.OwnsMany(
                lp => lp.Courses,
                cou => BuildCoursesModel(cou));
            
        }

        private void BuildCoursesModel(OwnedNavigationBuilder<LearningPath, Course> courseBuilder)
        {
            courseBuilder.Property(ca => ca.Level)
                    .HasConversion(LevelConverter);

            courseBuilder.Property(ca => ca.LearningPathId)
                .HasConversion(lpId => lpId.Value, lpGuid => new LearningPathId(lpGuid))
                .HasColumnName(nameof(Course.LearningPathId))
                .IsRequired();
            courseBuilder.Property(ca => ca.Id)
                .HasConversion(CourseIdConverter)
                .HasColumnName(nameof(Course.Id))
                .IsRequired();
            courseBuilder.HasKey(co => co.Id);

            courseBuilder.WithOwner().HasConstraintName(nameof(Course.LearningPathId));
            courseBuilder.OwnsMany(
                co => co.Lessons,
                le => BuildLessonModel(le)
                );
        }

        private void BuildLessonModel(OwnedNavigationBuilder<Course, Lesson>  lessonBuilder)
        {
            lessonBuilder.Property(ca => ca.Level)
                .HasConversion(LevelConverter);

            lessonBuilder.Property(ca => ca.CourseId)
                .HasConversion(CourseIdConverter)
                .HasColumnName(nameof(Lesson.CourseId)).IsRequired();
            lessonBuilder.WithOwner().HasConstraintName(nameof(Lesson.CourseId));
            lessonBuilder.Property(le => le.Id)
                .HasConversion(leId => leId.Value, leGuid => new LessonId(leGuid))
                .HasColumnName(nameof(Lesson.Id)).IsRequired();

            lessonBuilder.HasKey(le => le.Id);
        }
        private ValueConverter<Level, int> LevelConverter { get; } = new ValueConverter<Level, int>(
            level => level.Value,
            num => Level.Create(num));

        private ValueConverter<CourseId, Guid> CourseIdConverter { get; } = new ValueConverter<CourseId, Guid>(
            couresId => couresId.Value,
            guid => new CourseId(guid));
            
    }
}
