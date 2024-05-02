using Azure.Messaging.ServiceBus;
using Claims.Domain.Interfaces.Queues;

namespace Claims.Infrastructure.Queues;

public class CoverAuditMessagePublisher : MessagePublisher, ICoverAuditMessagePublisher
{
    public CoverAuditMessagePublisher(ServiceBusClient serviceBusClient) : base(serviceBusClient, Queues.CoverAudit)
    {
    }
}
