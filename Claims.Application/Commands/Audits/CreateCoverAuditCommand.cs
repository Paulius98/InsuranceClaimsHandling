using Claims.Domain.Entities.Audtiting;
using Claims.Domain.Enums;
using MediatR;

namespace Claims.Application.Commands.Audits
{
    public record CreateCoverAuditCommand(
        Guid CoverId,
        HttpRequestMethodType HttpRequestMethodType) : IRequest<CoverAudit>;
}
