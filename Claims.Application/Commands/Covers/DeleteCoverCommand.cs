using MediatR;

namespace Claims.Application.Commands.Covers
{
    public record DeleteCoverCommand(Guid Id) : INotification;
}
