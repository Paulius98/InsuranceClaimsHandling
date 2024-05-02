using Azure.Messaging.ServiceBus;
using Claims.Application.Queues.Receivers;
using Claims.Domain.Interfaces.Queues;
using Microsoft.Extensions.Logging;

namespace Claims.Infrastructure.Queues;

public  class ClaimAuditMessageReceiver : MessageReceiver, IClaimAuditMessageReceiver
{
    public ClaimAuditMessageReceiver(
        ServiceBusClient serviceBusClient,
        IAuditClaimReceiverHandler auditClaimIntegrationEventHandler,
        ILogger<ClaimAuditMessageReceiver> logger) 
        : base(serviceBusClient, Queues.ClaimAudit, auditClaimIntegrationEventHandler, logger)
    {
    }
}
