namespace Claims.Domain.Interfaces.Contexts;

public interface IContext
{
    Task SaveContextChangesAsync(CancellationToken cancellationToken = default);
}
