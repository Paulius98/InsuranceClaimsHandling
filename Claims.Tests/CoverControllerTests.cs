using Claims.Application.Queues.Models;
using Claims.Domain.Interfaces.Queues;
using Claims.Models.Responses;
using Moq;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Claims.IntegrationTests;

public class CoverControllerTests : IntegrationTest
{
    [Fact]
    public async Task GetAll_WithoutAnyPosts_ShouldReturnsEmptycollection()
    {
        // Arrange

        // Act
        var response = await TestClient.GetAsync(ApiRoutes.Covers);
        var result = await GetResultAsync<IEnumerable<CoverResponseDto>>(response);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAll_WithPosts_ShouldReturnCollection()
    {
        // Arrange
        var coverRequest = GetValidCoverRequestDto();
        await TestClient.PostAsJsonAsync(ApiRoutes.Covers, coverRequest);
        await TestClient.PostAsJsonAsync(ApiRoutes.Covers, coverRequest);
        var expectedItemsInCollection = 2;

        // Act
        var response = await TestClient.GetAsync(ApiRoutes.Covers);
        var result = await GetResultAsync<IEnumerable<CoverResponseDto>>(response);

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
        var coverRequest = GetValidCoverRequestDto();
        var coverResponse = await TestClient.PostAsJsonAsync(ApiRoutes.Covers, coverRequest);
        var cover = await GetResultAsync<CoverResponseDto>(coverResponse);
        var expectedStartDate = cover?.StartDate;
        var expectedEndDate = cover?.EndDate;
        var expectedCoverId = cover?.Id;

        // Act
        var response = await TestClient.GetAsync($"{ApiRoutes.Covers}/{cover?.Id}");
        var result = await GetResultAsync<CoverResponseDto>(response);

        // Assert
        response.EnsureSuccessStatusCode();

        Assert.NotNull(result);
        Assert.Equal(expectedCoverId, result.Id);
        Assert.Equal(expectedStartDate, result.StartDate);
        Assert.Equal(expectedEndDate, result.EndDate);
    }

    [Fact]
    public async Task Get_ReturnsNotFoundException_WhenIdNotExists()
    {
        // Arrange
        var expectedStatusCode = HttpStatusCode.NotFound;
        // Act
        var response = await TestClient.GetAsync($"{ApiRoutes.Covers}/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    [Fact]
    public async Task Post_ShouldCreateCover_WhenValidRequest()
    {
        // Arrange
        var coverRequest = GetValidCoverRequestDto();
        var expectedStartDate = coverRequest.StartDate;
        var expectedEndDate = coverRequest.EndDate;

        // Act
        var response = await TestClient.PostAsJsonAsync(ApiRoutes.Covers, coverRequest);
        var result = await GetResultAsync<CoverResponseDto>(response);

        // Assert
        response.EnsureSuccessStatusCode();

        Assert.NotNull(result?.Id);
        Assert.Equal(expectedStartDate, result.StartDate);
        Assert.Equal(expectedEndDate, result.EndDate);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenExists()
    {
        // Arrange
        var coverRequest = GetValidCoverRequestDto();
        var coverToDeleteResponse = await TestClient.PostAsJsonAsync(ApiRoutes.Covers, coverRequest);
        var coverToDelete = await GetResultAsync<CoverResponseDto>(coverToDeleteResponse);
        var expectedStatusCode = HttpStatusCode.NoContent;

        // Act
        var response = await TestClient.DeleteAsync($"{ApiRoutes.Covers}/{coverToDelete?.Id}");

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenStartDateInPast()
    {
        // Arrange
        var coverRequest = GetValidCoverRequestDto();
        coverRequest.StartDate = DateTimeOffset.UtcNow.AddDays(-1);
        var expectedStatusCode = HttpStatusCode.BadRequest;

        // Act
        var response = await TestClient.PostAsJsonAsync(ApiRoutes.Covers, coverRequest);

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequest_WhenDatePeriodExceeded()
    {
        // Arrange
        var coverRequest = GetValidCoverRequestDto();
        coverRequest.EndDate = DateTimeOffset.UtcNow.AddYears(2);
        var expectedStatusCode = HttpStatusCode.BadRequest;

        // Act
        var response = await TestClient.PostAsJsonAsync(ApiRoutes.Covers, coverRequest);

        // Assert
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}

