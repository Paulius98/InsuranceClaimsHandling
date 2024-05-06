using Claims.Application.Commands.Audits;
using Claims.Application.Queues.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Claims.Application.Queues.Receivers;

public class AuditClaimReceiverHandler : IAuditClaimReceiverHandler
{
    private readonly IServiceProvider _serviceProvider;

    public AuditClaimReceiverHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> ExecuteAsync(string message)
    {
        var claimAuditMessage = JsonSerializer.Deserialize<ClaimAuditQueueMessage>(message);
        if (claimAuditMessage?.Payload is null) return false;

        await using var scope = _serviceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AuditClaimReceiverHandler>>();

        var claimAudit = await mediator.Send(new CreateClaimAuditCommand(
            claimAuditMessage.Payload.ClaimId,
            claimAuditMessage.Payload.RequestMethod));

        logger.LogInformation($"Claim audit was created. {claimAudit}");

        return true;
    }
}
