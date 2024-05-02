using MediatR;

namespace Claims.Application.Commands.Claims;

public record DeleteClaimCommand(Guid Id) : INotification;
