using Azure.Messaging.ServiceBus;
using Claims.Domain.Interfaces.Queues;
using Microsoft.Extensions.Logging;

namespace Claims.Infrastructure.Queues;

public class ClaimAuditMessagePublisher : MessagePublisher, IClaimAuditMessagePublisher
{
    public ClaimAuditMessagePublisher(ServiceBusClient serviceBusClient, ILogger<ClaimAuditMessagePublisher> logger) 
        : base(serviceBusClient, Queues.ClaimAudit, logger)
    {
    }
}
