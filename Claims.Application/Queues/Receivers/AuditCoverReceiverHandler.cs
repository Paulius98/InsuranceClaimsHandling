using Claims.Application.Commands.Audits;
using Claims.Application.Queues.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Claims.Application.Queues.Receivers;

public class AuditCoverReceiverHandler : IAuditCoverReceiverHandler
{
    private readonly IServiceProvider _serviceProvider;

    public AuditCoverReceiverHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> ExecuteAsync(string message)
    {
        var coverAuditMessage = JsonSerializer.Deserialize<CoverAuditQueueMessage>(message);
        if (coverAuditMessage?.Payload is null) return false;

        await using var scope = _serviceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AuditCoverReceiverHandler>>();

        var coverAudit = await mediator.Send(new CreateCoverAuditCommand(
            coverAuditMessage.Payload.CoverId,
            coverAuditMessage.Payload.RequestMethod));

        logger.LogInformation($"Cover audit was created. {coverAudit}");

        return true;
    }
}
