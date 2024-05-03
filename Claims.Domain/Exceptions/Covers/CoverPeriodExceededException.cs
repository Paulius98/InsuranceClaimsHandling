namespace Claims.Domain.Exceptions.Covers;

public class CoverPeriodExceededException : DomainException
{
    public CoverPeriodExceededException() : base("Cover period exceeded.")
    {
    }
}