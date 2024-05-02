using Claims.Domain.Entities;
using Claims.Domain.Enums;
using Claims.Domain.Exceptions.Covers;
using Claims.Domain.Tests.Shared;
using System.Security.Claims;

namespace Claims.Domain.Tests
{
    public class CoverTests
    {
        [Theory, AutoMoqData]
        public void Create_ShouldCreateNewCover(
            DateTimeOffset startDate,
            DateTimeOffset endDate, 
            CoverType type)
        {
            // Arrange

            // Act 
            var cover = Cover.Create(startDate, endDate, type);

            // Assert
            Assert.NotEqual(Guid.Empty, cover.Id);
            Assert.Equal(startDate, cover.StartDate);
            Assert.Equal(endDate, cover.EndDate);
            Assert.Equal(type, cover.Type);
        }

        [Theory, AutoMoqData]
        public void ValidateDateRange_ShouldThrowException_WhenStartDateInThePast(
            DateTimeOffset endDate,
            CoverType type)
        {
            // Arrange
            var startDate = DateTimeOffset.UtcNow.AddHours(-1);

            var cover = Cover.Create(startDate, endDate, type);

            // Act
            void action() => cover.ValidateDateRange();

            // Assert
            Assert.Throws<CoverStartTimeInPastException>(action);
        }

        [Theory, AutoMoqData]
        public void ValidateDateRange_ShouldThrowException_WhenEndDateAfterStartDate(CoverType type)
        {
            // Arrange
            var startDate = DateTimeOffset.UtcNow.AddDays(1);
            var endDate = startDate.AddMinutes(-1);

            var cover = Cover.Create(startDate, endDate, type);

            // Act
            void action() => cover.ValidateDateRange();

            // Assert
            Assert.Throws<CoverDateRangeInvalidException>(action);
        }

        [Theory, AutoMoqData]
        public void ValidateDateRange_ShouldThrowException_WhenCoverPeriodDurationExceeds(
            CoverType type)
        {
            // Arrange
            var startDate = DateTimeOffset.UtcNow.AddDays(1);
            var endDate = startDate.AddYears(1).AddMinutes(1);

            var cover = Cover.Create(startDate, endDate, type);

            // Act
            void action() => cover.ValidateDateRange();

            // Assert
            Assert.Throws<CoverPeriodExceededException>(action);
        }

    }
}
