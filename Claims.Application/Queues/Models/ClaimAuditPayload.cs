using Claims.Domain.Enums;

namespace Claims.Application.IntegrationEventHandlers.Models;

public class ClaimAuditPayload
{
    public required Guid ClaimId { get; set; }
    public required HttpRequestMethodType RequestMethod { get; set; }
}
