using Azure.Messaging.ServiceBus;
using Claims.Domain.Interfaces.Queues;
using Microsoft.Extensions.Logging;

namespace Claims.Infrastructure.Queues;

public class CoverAuditMessagePublisher : MessagePublisher, ICoverAuditMessagePublisher
{
    public CoverAuditMessagePublisher(ServiceBusClient serviceBusClient, ILogger<CoverAuditMessagePublisher> logger)
        : base(serviceBusClient, Queues.CoverAudit, logger)
    {
    }
}
