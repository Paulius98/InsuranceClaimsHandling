using Claims.Domain.Enums;
using Claims.Domain.Events;
using Claims.Domain.Exceptions.Claims;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Claims.Domain.Entities;

public class Claim : AggregateRoot
{
    [BsonId]
    public Guid Id { get; private set; }

    [BsonElement("coverId")]
    public Guid CoverId { get; private set; }

    [BsonElement("created")]
    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset Created { get; private set; }

    [BsonElement("name")]
    public string Name { get; private set; } = null!;

    [BsonElement("claimType")]
    public ClaimType Type { get; private set; }

    [BsonElement("damageCost")]
    public decimal DamageCost { get; private set; }

    public Claim() { }

    private Claim(Guid coverId, DateTimeOffset created, string name, ClaimType type, decimal damageCost)
    {
        Id = Guid.NewGuid();
        CoverId = coverId;
        Created = created;
        Name = name; 
        Type = type;
        DamageCost = damageCost;

        AddEvent(new ClaimCreatedEvent(this, DateTimeOffset.UtcNow));
    }

    public static Claim Create(Guid coverId, DateTimeOffset created, string name, ClaimType type, decimal damageCost)
    {
        return new Claim(coverId, created, name, type, damageCost);
    }

    public void ValidateDamageCost()
    {
        if (DamageCost > 100000)
        {
            throw new DamageCostExceededException();
        }
    }

    public void ValidateDate(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (Created < startDate || Created > endDate)
        {
            throw new ClaimCreationDateInvalidException();
        }
    }

    public void Delete()
    {
        AddEvent(new ClaimDeletedEvent(this, DateTimeOffset.UtcNow));
    }
}