namespace Claims.Domain.Interfaces.Queues;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken) where T : class;
}
