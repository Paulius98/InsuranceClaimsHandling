using Claims.Application.Queues.Receivers;
using Claims.Domain.Interfaces.Contexts;
using Claims.Domain.Interfaces.Queues;
using Claims.Domain.Interfaces.Repositories;
using Claims.Infrastructure.Persistence;
using Claims.Infrastructure.Persistence.Repositories;
using Claims.Infrastructure.Queues;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Reflection;

namespace Claims.Extensions;

public static class ServiceRegistrations
{
    public static IServiceCollection AddContexts(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddDbContext<AuditContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuditDatabase"));
            }).AddScoped<IAuditContext, AuditContext>();

        var mongoClient = new MongoClient(configuration.GetConnectionString("ClaimsDatabase"));
        var mongoDatabase = mongoClient.GetDatabase(configuration["ClaimsDatabase:Name"]);
        services
            .AddDbContext<ClaimsContext>(options =>
            { 
                options.UseMongoDB(mongoDatabase.Client, mongoDatabase.DatabaseNamespace.DatabaseName);
            }).AddScoped<IClaimsContext, ClaimsContext>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IClaimAuditRepository, ClaimAuditRepository>()
            .AddScoped<ICoverAuditRepository, CoverAuditRepository>()
            .AddScoped<IClaimsRepository, ClaimsRepository>()
            .AddScoped<ICoverRepository, CoversRepository>();

        return services;
    }
    public static IServiceCollection AddEventBus(this IServiceCollection services, string? connectionString)
    {
        services.AddAzureClients(builder =>
        {
            builder.AddServiceBusClient(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddMessagePublishers(this IServiceCollection services)
    {
        services
            .AddSingleton<IClaimAuditMessagePublisher, ClaimAuditMessagePublisher>()
            .AddSingleton<ICoverAuditMessagePublisher, CoverAuditMessagePublisher>();

        return services;
    }

    public static IServiceCollection AddIntegrationEventReceivers(this IServiceCollection services)
    {
        services
            .AddSingleton<IAuditClaimReceiverHandler, AuditClaimReceiverHandler>()
            .AddSingleton<IAuditCoverReceiverHandler, AuditCoverReceiverHandler>();

        return services;
    }

    public static IServiceCollection AddMessageReceivers(this IServiceCollection services)
    {
        services
            .AddSingleton<IClaimAuditMessageReceiver, ClaimAuditMessageReceiver>()
            .AddSingleton<ICoverAuditMessageReceiver, CoverAuditMessageReceiver>();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        var info = new OpenApiInfo()
        {
            Title = "Insurance Claims Handling API",
            Version = "v1",
            Description = "This API provides endpoints for managing insurance claims and covers. It allows users to retrieve, create, and delete both claims and covers."
        };

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", info);

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}
