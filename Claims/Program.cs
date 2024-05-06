using Claims;
using Claims.Application.Queues.Models;
using Claims.Extensions;
using Claims.Infrastructure.Persistence;
using Claims.MIddleware.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services
    .AddContexts(builder.Configuration)
    .AddRepositories()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ClaimAuditQueueMessage).Assembly))
    .AddRepositories()
    .AddEndpointsApiExplorer()
    .AddEventBus(builder.Configuration.GetConnectionString("ServiceBus"))
    .AddMessagePublishers()
    .AddMessageReceivers()
    .AddIntegrationEventReceivers()
    .AddHostedService<HostedService>()
    .AddSwagger();

var app = builder.Build();

app.UseErrorHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    if (configuration["State"] != "IntegrationTesting")
    {
        var context = scope.ServiceProvider.GetRequiredService<AuditContext>();
        context.Database.Migrate();
    }
}

app.Run();

public partial class Program { }
