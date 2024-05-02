using Claims.Application.Commands.Audits;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
        var claimAuditMessage = JsonConvert.DeserializeObject<ClaimAuditQueueMessage>(message);
        if (claimAuditMessage?.Payload is null) return false;

        await using var scope = _serviceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Publish(new CreateClaimAuditCommand(
            claimAuditMessage.Payload.ClaimId,
            claimAuditMessage.Payload.RequestMethod));

        return true;
    }
}
