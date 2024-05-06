using Claims.Domain.Enums;
using Claims.Domain.Interfaces.Queues;
using Claims.Infrastructure.Persistence;
using Claims.Models.Requests;
using Claims.Models.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Claims.IntegrationTests;

public class IntegrationTest : IDisposable
{
    protected readonly HttpClient TestClient;
    private readonly WebApplicationFactory<Program> _appFactory;
    private readonly Mock<IClaimAuditMessagePublisher> _claimAuditMessagePublisherMock;
    private readonly Mock<ICoverAuditMessagePublisher> _coverAuditMessagePublisherMock;


    private readonly string _claimDbName;
    private readonly string _auditDbName;

    public IntegrationTest()
    {
        _claimDbName = Guid.NewGuid().ToString();
        _auditDbName = Guid.NewGuid().ToString();
        _claimAuditMessagePublisherMock = new Mock<IClaimAuditMessagePublisher>();
        _coverAuditMessagePublisherMock = new Mock<ICoverAuditMessagePublisher>();

        _appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices((context, services) =>
            {
                context.Configuration["State"] = "IntegrationTesting";

                var auditContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AuditContext>));
                
                var claimsContext = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ClaimsContext>));

                if (auditContext != null)
                    services.Remove(auditContext);

                if (claimsContext != null)
                    services.Remove(claimsContext);

                services.AddDbContext<AuditContext>(options =>
                {
                    options.UseInMemoryDatabase(_auditDbName);
                });

                services.AddDbContext<ClaimsContext>(options =>
                {
                    options.UseInMemoryDatabase(_claimDbName);
                });

                services.AddScoped(_ => _claimAuditMessagePublisherMock.Object);
                services.AddScoped(_ => _coverAuditMessagePublisherMock.Object);

            });
        });

        TestClient = _appFactory.CreateClient();
    }

    protected CoverRequestDto GetValidCoverRequestDto()
    {
        var now = DateTimeOffset.UtcNow;

        return new CoverRequestDto
        {
            StartDate = now.AddDays(1),
            EndDate = now.AddYears(1),
            Type = CoverType.ContainerShip
        };
    }

    protected async Task<ClaimRequestDto> GetValidClaimRequestDtoAsync()
    {
        var cover = await GetResultAsync<CoverResponseDto>(await TestClient.PostAsJsonAsync(ApiRoutes.Covers, GetValidCoverRequestDto()));
        return new ClaimRequestDto
        {
            CoverId = cover!.Id,
            Created = DateTimeOffset.UtcNow.AddDays(2),
            Name = "testClaimName",
            Type = ClaimType.Collision,
            DamageCost = 50000
        };
    }


    protected static async Task<T?> GetResultAsync<T>(HttpResponseMessage? response) where T : class
    {
        if (response is null) return null;
        var responseAsString = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions { 
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        return JsonSerializer.Deserialize<T>(responseAsString, options);
    }

    public virtual void Dispose()
    {
        _appFactory.Dispose();
    }
}
