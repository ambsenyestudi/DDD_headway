using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace EasyLanguageLearning.Infrastructure
{
    public class DataContext : DbContext
    {

        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<LanguageCatalog> LanguageCatalogs { get; set; }
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            BuildLearningPathModel(modelBuilder);
            BuildLangaugeCatalogModel(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }

        private void BuildLangaugeCatalogModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LanguageCatalog>(entity =>
                entity.HasKey(e => e.Id));
            var languageCatalogBuilder = modelBuilder.Entity<LanguageCatalog>();
            languageCatalogBuilder.ToTable(nameof(LanguageCatalogs));
            languageCatalogBuilder.Property(lc => lc.Id)
                .HasConversion(lcId => lcId.Value, guid => new LanguageCatalogId(guid))
                .IsRequired();
            languageCatalogBuilder.Property(lc => lc.Iso)
                .HasConversion(IsoConverter);
                                   
            languageCatalogBuilder.OwnsMany(
                    lc => lc.Items,
                    ll => BuildLearningLanguageModel(ll)
                );
            
        }
        
        private void BuildLearningLanguageModel(OwnedNavigationBuilder<LanguageCatalog, LearningLanguage> leaningLangaugeModel)
        {
            leaningLangaugeModel.Property(ll => ll.LanguageCatalogId)
                .HasConversion(llId => llId.Value, lpGuid => new LanguageCatalogId(lpGuid))
                .HasColumnName(nameof(LearningLanguage.LanguageCatalogId))
                .IsRequired();
            leaningLangaugeModel.Property(ll => ll.Id)
                .HasConversion(LearningLanguageIdConverter)
                .HasColumnName(nameof(LearningLanguage.Id))
                .IsRequired();
            leaningLangaugeModel.Property(lc => lc.Iso)
                .HasConversion(IsoConverter);

            leaningLangaugeModel.HasKey(co => co.Id);

            leaningLangaugeModel.WithOwner().HasConstraintName(nameof(LearningLanguage.LanguageCatalogId));
        }

        private void BuildLearningPathModel(ModelBuilder modelBuilder)
        {
            var learningPathBuilder = modelBuilder.Entity<LearningPath>();
            learningPathBuilder.ToTable(nameof(LearningPaths));
            learningPathBuilder.Property(x => x.Id)
                .HasConversion(x => x.Value, v => new LearningPathId(v))
                .HasColumnName(nameof(LearningPath.Id))
                .IsRequired();

            learningPathBuilder.Property(x => x.LearningLanguageIso)
                .HasConversion(IsoConverter);
            learningPathBuilder.Property(x => x.MotherLanguageIso)
                .HasConversion(IsoConverter);
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

        private ValueConverter<LearningLanguageId, Guid> LearningLanguageIdConverter { get; } = new ValueConverter<LearningLanguageId, Guid>(
            couresId => couresId.Value,
            guid => new LearningLanguageId(guid));

        private ValueConverter<Iso, string> IsoConverter { get; } = new ValueConverter<Iso, string>(
            iso => iso.IsoCode,
            isoCode => Iso.CreateIso(Enum.Parse<IsoCodes>(isoCode))
        );
    }
}
