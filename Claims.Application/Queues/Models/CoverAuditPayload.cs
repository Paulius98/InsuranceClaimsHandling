using Claims.Domain.Enums;

namespace Claims.Application.Queues.Models;

public class CoverAuditPayload
{
    public required Guid CoverId { get; set; }
    public required HttpRequestMethodType RequestMethod { get; set; }
}
