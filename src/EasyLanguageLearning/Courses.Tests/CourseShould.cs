using Courses.Domain;
using Courses.Domain.Languages;
using Courses.Domain.Translations;
using EasyLanguageLearning.Domain.Shared.Kernel.Languages;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Courses.Tests
{
    public class CourseShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(4)]
        public void NotGetCourseWhenLevelNotInRange1_3(int level)
        {
            var motherLang = Language.CreateFromNameAndIso("Español", Iso.CreateIso(IsoCodes.es));
            var learnLang = Language.CreateFromNameAndIso("English", Iso.CreateIso(IsoCodes.en));
            var sut = new Course(Guid.Empty, motherLang, learnLang);
            var translationLookUpMock = new Mock<ITranslationLookUp>();
            translationLookUpMock
                .Setup(x => x.Translate(It.IsAny<Iso>(), It.IsAny<Iso>(), It.IsAny<string>()))
                .Returns(string.Empty);
            Assert.Throws<ArgumentException>(() => sut.SetName(level, translationLookUpMock.Object));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void GetCouresContainingLevelInName(int level)
        {
            
            var exptectedLevel = level.ToString();
            var motherLang = Language.CreateFromNameAndIso("Español", Iso.CreateIso(IsoCodes.es));
            var learnLang = Language.CreateFromNameAndIso("English", Iso.CreateIso(IsoCodes.en));
            var sut = new Course(Guid.Empty, motherLang, learnLang);
            var translationLookUpMock = new Mock<ITranslationLookUp>();
            translationLookUpMock
                .Setup(x => x.Translate(It.IsAny<Iso>(), It.IsAny<Iso>(), It.IsAny<string>()))
                .Returns(string.Empty);

            sut.SetName(level, translationLookUpMock.Object);
            Assert.Contains(exptectedLevel, sut.Name);
        }
    }
}
