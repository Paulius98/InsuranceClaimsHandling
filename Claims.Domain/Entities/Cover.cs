using Claims.Domain.Calculations;
using Claims.Domain.Enums;
using Claims.Domain.Events;
using Claims.Domain.Exceptions.Covers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Claims.Domain.Entities;

public class Cover : AggregateRoot
{
    [BsonId]
    public Guid Id { get; private set; }

    [BsonElement("startDate")]
    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset StartDate { get; private set; }

    [BsonElement("endDate")]
    [BsonRepresentation(BsonType.String)]
    public DateTimeOffset EndDate { get; private set; }

    [BsonElement("claimType")]
    public CoverType Type { get; private set; }

    [BsonElement("premium")]
    public decimal Premium { get; private set; }

    public Cover() { }

    private Cover(DateTimeOffset startDate, DateTimeOffset endDate, CoverType type)
    {
        Id = Guid.NewGuid();
        StartDate = startDate;
        EndDate = endDate;
        Type = type;
        Premium = PremiumCalculator.Compute(startDate, endDate, type);

        AddEvent(new CoverCreatedEvent(this, DateTimeOffset.UtcNow));
    }

    public static Cover Create(DateTimeOffset startDate, DateTimeOffset endDate, CoverType type)
    {
        return new Cover(startDate, endDate, type);
    }

    public void Delete()
    {
        AddEvent(new CoverDeletedEvent(this, DateTimeOffset.UtcNow));
    }

    public void ValidateDateRange()
    {
        ValidateStartDateInPast();
        ValidateEndDateAfterStartDate();
        ValidateCoverPeriodDuration();
    }

    private void ValidateStartDateInPast()
    {
        if (StartDate < DateTimeOffset.UtcNow)
        {
            throw new CoverStartTimeInPastException();
        }
    }

    private void ValidateEndDateAfterStartDate()
    {
        if (StartDate > EndDate)
        {
            throw new CoverDateRangeInvalidException();
        }
    }

    private void ValidateCoverPeriodDuration()
    {
        if (StartDate.AddYears(1) < EndDate)
        {
            throw new CoverPeriodExceededException();
        }
    }
}
