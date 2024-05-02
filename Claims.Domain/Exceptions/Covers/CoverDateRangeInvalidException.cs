namespace Claims.Domain.Exceptions.Covers;

public class CoverDateRangeInvalidException : DomainException
{
    public CoverDateRangeInvalidException() : base("The cover date range is invalid.")
    {
    }
}
