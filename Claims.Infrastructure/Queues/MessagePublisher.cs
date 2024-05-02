using Azure.Messaging.ServiceBus;
using Claims.Domain.Interfaces.Queues;
using Newtonsoft.Json;

namespace Claims.Infrastructure.Queues;

public abstract class MessagePublisher : IMessagePublisher
{
    private readonly ServiceBusSender _serviceBusSender;

    public MessagePublisher(ServiceBusClient serviceBusClient, string queueName)
    {
        _serviceBusSender = serviceBusClient.CreateSender(queueName);
    }

    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken) where T : class
    {
        var message = new ServiceBusMessage(JsonConvert.SerializeObject(integrationEvent));
        await _serviceBusSender.SendMessageAsync(message, cancellationToken);
    }
}
