using Claims.Domain.Entities;
using Claims.Domain.Enums;

namespace Claims.Models.Responses;

public class ClaimResponseDto
{
    public  Guid Id { get; set; }
    public Guid CoverId { get; set; }
    public DateTimeOffset Created { get; set; }
    public required string Name { get; set; }
    public ClaimType Type { get; set; }
    public decimal DamageCost { get; set; }

    public static ClaimResponseDto FromDomain(Claim claim)
    {
        return new ClaimResponseDto
        {
            Id = claim.Id,
            CoverId = claim.CoverId, 
            Created = claim.Created,
            Name = claim.Name, 
            Type = claim.Type,
            DamageCost = claim.DamageCost
        };
    }

    public override string ToString()
    {
        return $"Id: {Id}, CoverId: {CoverId}, Created: {Created}, Name: {Name}, Type: {Type}, DamageCost: {DamageCost}";
    }
}