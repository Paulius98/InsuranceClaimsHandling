using Azure.Messaging.ServiceBus;
using Claims.Domain.Interfaces.Queues;
using Microsoft.Extensions.Logging;

namespace Claims.Infrastructure.Queues;

public abstract class MessageReceiver : 
    IClaimAuditMessageReceiver,
    ICoverAuditMessageReceiver
{
    private readonly ServiceBusProcessor _processor;
    private readonly IMessageReceiverHandler _handler;
    private readonly ILogger<MessageReceiver> _logger;
    private readonly string _queueName;

    public MessageReceiver(
        ServiceBusClient serviceBusClient,
        string queueName,
        IMessageReceiverHandler handler,
        ILogger<MessageReceiver> logger)
    {
        var processorOptions = new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false
        };

        _processor = serviceBusClient.CreateProcessor(queueName, processorOptions);
        _handler = handler;
        _queueName = queueName;
        _logger = logger;
    }

    public async Task RegisterMessageReceiverAsync(CancellationToken cancellationToken = default)
    {
        _processor.ProcessMessageAsync += HandleMessageAsync;
        _processor.ProcessErrorAsync += HandleErrorAsync;

        await _processor.StartProcessingAsync(cancellationToken);
    }

    public async Task UnregisterMessageReceiverAsync(CancellationToken cancellationToken = default)
    {
        await _processor.StopProcessingAsync(cancellationToken);
    }

    public async Task CloseAsync(CancellationToken cancellationToken = default)
    {
        await _processor.CloseAsync(cancellationToken);
    }

    private async Task HandleMessageAsync(ProcessMessageEventArgs args)
    {
        var message = args.Message.Body.ToString();


        if (await _handler.ExecuteAsync(message))
        {
            await args.CompleteMessageAsync(args.Message);
        }
    }

    private Task HandleErrorAsync(ProcessErrorEventArgs args)
    {
        _logger.LogError($"Failed receiving message from queue: {_queueName}", args.Exception);
        return Task.CompletedTask;
    }
}
