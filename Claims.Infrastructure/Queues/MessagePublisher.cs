using Azure.Messaging.ServiceBus;
using Claims.Domain.Interfaces.Queues;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Claims.Infrastructure.Queues;

public abstract class MessagePublisher : IMessagePublisher
{
    private readonly ServiceBusSender _serviceBusSender;
    private readonly string _queueName;
    private readonly ILogger<MessagePublisher> _logger;

    public MessagePublisher(ServiceBusClient serviceBusClient, string queueName, ILogger<MessagePublisher> logger)
    {
        _serviceBusSender = serviceBusClient.CreateSender(queueName);
        _queueName = queueName;
        _logger = logger;
    }

    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken) where T : class
    {
        var message = JsonSerializer.Serialize(integrationEvent);
        var sbMessage = new ServiceBusMessage(message);
        await _serviceBusSender.SendMessageAsync(sbMessage, cancellationToken);

        _logger.LogInformation($"Sent message to queue: {_queueName}. Message: {message}");
    }
}
