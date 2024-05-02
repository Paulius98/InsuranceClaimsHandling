using Claims.Domain.Enums;

namespace Claims.Models.Requests;

public class ClaimRequestDto
{
    public Guid CoverId { get; set; }

    public DateTimeOffset Created { get; set; }

    public required string Name { get; set; }

    public ClaimType Type { get; set; }

    public decimal DamageCost { get; set; }
}
