using Azure.Messaging.ServiceBus;
using Claims.Domain.Interfaces.Queues;

namespace Claims.Infrastructure.Queues;

public class ClaimAuditMessagePublisher : MessagePublisher, IClaimAuditMessagePublisher
{
    public ClaimAuditMessagePublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Queues.ClaimAudit)
    {
    }
}
