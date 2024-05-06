using Claims.Domain.Enums;
using MediatR;

namespace Claims.Application.Commands.Audits;

public record PublishCoverAuditCommand(Guid CoverId, HttpRequestMethodType HttpRequestMethodType) : INotification;