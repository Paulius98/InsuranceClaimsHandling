using Claims.Domain.Entities;
using Claims.Domain.Enums;
using Claims.Domain.Exceptions.Claims;
using Claims.Domain.Tests.Shared;

namespace Claims.Domain.Tests;

public class ClaimTests
{
    [Theory, AutoMoqData]
    public void Create_ShouldCreateNewClaim(
        Guid coverId,
        DateTimeOffset created,
        string name,
        ClaimType type,
        decimal damageCost)
    {
        // Arrange

        // Act 
        var claim = Claim.Create(coverId, created, name, type, damageCost);

        // Assert
        Assert.NotEqual(Guid.Empty, claim.Id);
        Assert.Equal(coverId, claim.CoverId);
        Assert.Equal(created, claim.Created);
        Assert.Equal(name, claim.Name);
        Assert.Equal(type, claim.Type);
        Assert.Equal(damageCost, claim.DamageCost);
    }

    [Theory, AutoMoqData]
    public void ValidateDamageCost_ShouldThrowException_WhenDamageCostExceeds(
        Guid coverId,
        DateTimeOffset created,
        string name,
        ClaimType type)
    {
        // Arrange
        var damageCost = 1000001;
        var claim = Claim.Create(coverId, created, name, type, damageCost);

        // Act
        void action() => claim.ValidateDamageCost();

        // Assert
        Assert.Throws<DamageCostExceededException>(action);
    }

    [Theory, AutoMoqData]
    public void ValidateDate_ShouldThrowsException_WhenCreationDateNotInRange(
        Guid coverId,
        DateTimeOffset created,
        string name,
        ClaimType type,
        decimal damageCost)
    {
        // Arrange
        var startDate1 = created.AddHours(1);
        var endDate1 = created.AddHours(2);

        var startDate2 = created.AddHours(-2);
        var endDate2 = created.AddHours(-1);

        var claim = Claim.Create(coverId, created, name, type, damageCost);

        // Act
        void action1() => claim.ValidateDate(startDate1, endDate1);
        void action2() => claim.ValidateDate(startDate2, endDate2);

        // Assert
        Assert.Throws<ClaimCreationDateInvalidException>(action1);
        Assert.Throws<ClaimCreationDateInvalidException>(action2);
    }
}
