using Claims.IntegrationTests;
using Claims.Models.Requests;
using Claims.Models.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Claims.Tests;

public class ClaimsControllerTests : IntegrationTest
{
    [Fact]
    public async Task GetAll_WithoutAnyPosts_ShouldReturnsEmptyCollection()
    {
        // Arrange

        // Act
        var response = await TestClient.GetAsync("/Claims");
        var result = await GetResultAsync<IEnumerable<ClaimResponseDto>>(response);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAll_WithPosts_ShouldReturnCollection()
    {
        // Arrange
        var claimRequest = await GetValidClaimRequestDtoAsync();
        await TestClient.PostAsJsonAsync("/Claims", claimRequest);
        await TestClient.PostAsJsonAsync("/Claims", claimRequest);
        var expectedItemsInCollection = 2;

        // Act
        var response = await TestClient.GetAsync("/Claims");
        var result = await GetResultAsync<IEnumerable<ClaimResponseDto>>(response);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(expectedItemsInCollection, result.Count());
    }

    [Fact]
    public async Task Get_ReturnsPost_WhenPostExists()
    {
        // Arrange
        var claimRequest = await GetValidClaimRequestDtoAsync();
        var claim = await GetResultAsync<ClaimResponseDto>(await TestClient.PostAsJsonAsync("/Claims", claimRequest));

        // Act
        var response = await TestClient.GetAsync($"/Claims/{claim?.Id}");
        var result = await GetResultAsync<ClaimResponseDto>(response);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.Equal(claim?.Id, result.Id);
        AssertClaimResponse(claimRequest, result);
    }

    [Fact]
    public async Task Get_ReturnsNotFoundException_WhenIdNotExists()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.NotFound;
        // Act
        var response = await TestClient.GetAsync($"/Claims/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    [Fact]
    public async Task Post_ShouldCreateClaim_WhenValidRequest()
    {
        // Arrange
        var claimRequest = await GetValidClaimRequestDtoAsync();

        // Act
        var response = await TestClient.PostAsJsonAsync("/Claims", claimRequest);
        var result = await GetResultAsync<ClaimResponseDto>(response);

        // Assert
        response.EnsureSuccessStatusCode();

        Assert.NotNull(result?.Id);
        AssertClaimResponse(claimRequest, result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenExists()
    {
        // Arrange
        var claimRequest = await GetValidClaimRequestDtoAsync();
        var claimToDeleteResponse = await TestClient.PostAsJsonAsync("/Claims", claimRequest);
        var claimToDelete = await GetResultAsync<ClaimResponseDto>(claimToDeleteResponse);
        var expectedStatusCode = HttpStatusCode.NoContent;

        // Act
        var response = await TestClient.DeleteAsync($"/Claims/{claimToDelete?.Id}");

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenClaimCreationDateInvalid()
    {
        // Arrange
        var claimRequest = await GetValidClaimRequestDtoAsync();
        claimRequest.Created = DateTimeOffset.UtcNow.AddDays(-2);
        var expectedStatusCode = HttpStatusCode.BadRequest;

        // Act
        var response = await TestClient.PostAsJsonAsync("/Claims", claimRequest);

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenDamageCostExceeded()
    {
        // Arrange
        var claimRequest = await GetValidClaimRequestDtoAsync();
        claimRequest.DamageCost = 100001;
        var expectedStatusCode = HttpStatusCode.BadRequest;

        // Act
        var response = await TestClient.PostAsJsonAsync("/Claims", claimRequest);

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    private void AssertClaimResponse(ClaimRequestDto claimRequest, ClaimResponseDto claimResponse)
    {
        var expectedType = claimRequest.Type;
        var expectedCoverId = claimRequest.CoverId;
        var expectedCreationDate = claimRequest.Created;
        var expectedDamageCost = claimRequest.DamageCost;
        var expectedName = claimRequest.Name;

        Assert.Equal(expectedType, claimResponse.Type);
        Assert.Equal(expectedCoverId, claimResponse.CoverId);
        Assert.Equal(expectedCreationDate, claimResponse.Created);
        Assert.Equal(expectedDamageCost, claimResponse.DamageCost);
        Assert.Equal(expectedName, claimResponse.Name);
    }
}
