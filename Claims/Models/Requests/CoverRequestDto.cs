using Claims.Domain.Enums;

namespace Claims.Models.Requests;

public class CoverRequestDto
{
    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public CoverType Type { get; set; }
}
