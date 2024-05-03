namespace Claims.Domain.Exceptions.Covers;

public class CoverDateRangeInvalidException : DomainException
{
    public CoverDateRangeInvalidException() : base("Cover date range is invalid.")
    {
    }
}
