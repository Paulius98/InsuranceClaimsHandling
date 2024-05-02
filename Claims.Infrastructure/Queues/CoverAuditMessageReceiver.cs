using Azure.Messaging.ServiceBus;
using Claims.Application.Queues.Receivers;
using Claims.Domain.Interfaces.Queues;
using Microsoft.Extensions.Logging;

namespace Claims.Infrastructure.Queues;

public class CoverAuditMessageReceiver : MessageReceiver, ICoverAuditMessageReceiver
{
    public CoverAuditMessageReceiver(
        ServiceBusClient serviceBusClient,
        IAuditCoverReceiverHandler handler, 
        ILogger<CoverAuditMessageReceiver> logger) 
        : base(serviceBusClient, Queues.CoverAudit, handler, logger)
    {
    }
}
