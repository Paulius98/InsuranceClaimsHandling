using Claims.Domain.Enums;
using MediatR;

namespace Claims.Application.Commands.Audits;

public record PublishClaimAuditCommand(Guid ClaimId, HttpRequestMethodType HttpRequestMethodType) : INotification;
