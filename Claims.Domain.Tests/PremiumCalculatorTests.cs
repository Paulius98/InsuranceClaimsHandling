using Claims.Domain.Calculations;
using Claims.Domain.Enums;
using Claims.Domain.Tests.Shared;

namespace Claims.Domain.Tests
{
    public class PremiumCalculatorTests
    {
        [Theory]
        [InlineAutoMoqData(CoverType.Yacht, 15, 20625)]
        [InlineAutoMoqData(CoverType.Yacht, 61, 81743.75)]
        [InlineAutoMoqData(CoverType.Yacht, 275, 357362.5)]
        [InlineAutoMoqData(CoverType.PassengerShip, 15, 22500)]
        [InlineAutoMoqData(CoverType.PassengerShip, 61, 90570)]
        [InlineAutoMoqData(CoverType.PassengerShip, 275, 403725)]
        [InlineAutoMoqData(CoverType.Tanker, 15, 28125)]
        [InlineAutoMoqData(CoverType.Tanker, 61, 113212.5)]
        [InlineAutoMoqData(CoverType.Tanker, 275, 504656.25)]
        [InlineAutoMoqData(CoverType.ContainerShip, 15, 24375)]
        [InlineAutoMoqData(CoverType.ContainerShip, 61, 98117.5)]
        [InlineAutoMoqData(CoverType.ContainerShip, 275, 437368.75)]
        [InlineAutoMoqData(CoverType.BulkCarrier, 15, 24375)]
        [InlineAutoMoqData(CoverType.BulkCarrier, 61, 98117.5)]
        [InlineAutoMoqData(CoverType.BulkCarrier, 275, 437368.75)]
        public void ComputePremium(CoverType coverType, int dayCount, decimal expectedPremium)
        {
            // Arrange 
            var startDate = DateTimeOffset.Parse("2024-04-30");
            var endDate = startDate.AddDays(dayCount);

            // Act
            var premium = PremiumCalculator.Compute(startDate, endDate, coverType);

            // Assert
            Assert.Equal(expectedPremium, premium);
        }
    }
}
