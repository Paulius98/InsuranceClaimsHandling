namespace Claims.Domain.Interfaces.Queues;

public interface IMessageReceiver
{
    Task RegisterMessageReceiverAsync(CancellationToken cancellationToken = default);
    Task UnregisterMessageReceiverAsync(CancellationToken cancellationToken = default);
    Task CloseAsync(CancellationToken cancellationToken = default);
}
