using Claims.Domain.Entities;
using Claims.Domain.Enums;

namespace Claims.Models.Responses;

public class CoverResponseDto
{
    public Guid Id { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public CoverType Type { get; set; }
    public decimal Premium { get; set; }

    public static CoverResponseDto FromDomain(Cover cover)
    {
        return new CoverResponseDto
        {
            Id = cover.Id,
            StartDate = cover.StartDate,
            EndDate = cover.EndDate,
            Type = cover.Type,
            Premium = cover.Premium
        };
    }

    public override string ToString()
    {
        return $"Id: {Id}, StartDate: {StartDate}, EndDate: {EndDate}, Type: {Type}, Premium: {Premium}";
    }
}
