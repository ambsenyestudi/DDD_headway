using EasyLanguageLearning.Domain.LanguageCatalogs;
using EasyLanguageLearning.Domain.LearningPaths;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace EasyLanguageLearning.Infrastructure
{
    public static class DataModelConverters
    {
        public static ValueConverter<Level, int> LevelConverter { get; } = new ValueConverter<Level, int>(
           level => level.Value,
           num => Level.Create(num));

        public static ValueConverter<CourseId, Guid> CourseIdConverter { get; } = new ValueConverter<CourseId, Guid>(
            couresId => couresId.Value,
            guid => new CourseId(guid));

        public static ValueConverter<LearningLanguageId, Guid> LearningLanguageIdConverter { get; } = new ValueConverter<LearningLanguageId, Guid>(
            couresId => couresId.Value,
            guid => new LearningLanguageId(guid));

        
        public static ValueConverter<Iso, string> IsoConverter { get; } = new ValueConverter<Iso, string>(
            iso => iso.IsoCode,
            isoCode => Iso.CreateIso(Enum.Parse<IsoCodes>(isoCode))
        );

        public static ValueConverter<LessonId, Guid> LessonIdConverter { get; } = new ValueConverter<LessonId, Guid>(
            couresId => couresId.Value,
            guid => new LessonId(guid));

    }
}
