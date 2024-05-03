namespace Claims.Domain.Interfaces.Queues;

public interface IMessageReceiverHandler
{
    Task<bool> ExecuteAsync(string message);
}
