using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using conv = EasyLanguageLearning.Infrastructure.DataModelConverters;

namespace EasyLanguageLearning.Infrastructure.LearningPaths
{
    public static class LearningPathModelingExtensions
    {
        public static void BuildLearningPathModel(this ModelBuilder modelBuilder)
        {
            var learningPathBuilder = modelBuilder.Entity<LearningPath>();
            learningPathBuilder.ToTable(nameof(LearningPaths));
            learningPathBuilder.Property(x => x.Id)
                .HasConversion(x => x.Value, v => new LearningPathId(v))
                .HasColumnName(nameof(LearningPath.Id))
                .IsRequired();

            learningPathBuilder.Property(x => x.LearningLanguageIso)
                .HasConversion(conv.IsoConverter);
            learningPathBuilder.Property(x => x.MotherLanguageIso)
                .HasConversion(conv.IsoConverter);
            modelBuilder.Entity<LearningPath>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            learningPathBuilder.OwnsMany(
                lp => lp.Courses,
                cou => cou.BuildCoursesModel());

        }

        private static void BuildCoursesModel(this OwnedNavigationBuilder<LearningPath, Course> courseBuilder)
        {
            courseBuilder.Property(ca => ca.Level)
                    .HasConversion(conv.LevelConverter);

            courseBuilder.Property(ca => ca.LearningPathId)
                .HasConversion(lpId => lpId.Value, lpGuid => new LearningPathId(lpGuid))
                .HasColumnName(nameof(Course.LearningPathId))
                .IsRequired();
            courseBuilder.Property(ca => ca.Id)
                .HasConversion(conv.CourseIdConverter)
                .HasColumnName(nameof(Course.Id))
                .IsRequired();
            courseBuilder.HasKey(co => co.Id);

            courseBuilder.WithOwner().HasConstraintName(nameof(Course.LearningPathId));
            courseBuilder.OwnsMany(
                co => co.Lessons,
                le => le.BuildLessonModel()
                );
        }

        private static void BuildLessonModel(this OwnedNavigationBuilder<Course, Lesson> lessonBuilder)
        {
            lessonBuilder.Property(ca => ca.Level)
                .HasConversion(conv.LevelConverter);

            lessonBuilder.Property(ca => ca.CourseId)
                .HasConversion(conv.CourseIdConverter)
                .HasColumnName(nameof(Lesson.CourseId)).IsRequired();
            lessonBuilder.WithOwner().HasConstraintName(nameof(Lesson.CourseId));
            lessonBuilder.Property(le => le.Id)
                .HasConversion(conv.LessonIdConverter)
                .HasColumnName(nameof(Lesson.Id)).IsRequired();

            lessonBuilder.HasKey(le => le.Id);
        }
    }
}
