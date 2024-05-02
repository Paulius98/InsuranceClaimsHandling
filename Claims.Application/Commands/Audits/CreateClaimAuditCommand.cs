using Claims.Domain.Enums;
using MediatR;

namespace Claims.Application.Commands.Audits;

public record CreateClaimAuditCommand(
    Guid ClaimId,
    HttpRequestMethodType HttpRequestMethodType) : INotification;
