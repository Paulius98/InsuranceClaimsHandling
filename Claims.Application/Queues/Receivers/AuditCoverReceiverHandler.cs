using Claims.Application.Commands.Audits;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
        var coverAuditMessage = JsonConvert.DeserializeObject<CoverAuditQueueMessage>(message);
        if (coverAuditMessage?.Payload is null) return false;

        await using var scope = _serviceProvider.CreateAsyncScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        await mediator.Publish(new CreateCoverAuditCommand(
            coverAuditMessage.Payload.CoverId,
            coverAuditMessage.Payload.RequestMethod));

        return true;
    }
}
