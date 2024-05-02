namespace Claims.Domain.Exceptions.Covers;

public class CoverPeriodExceededException : DomainException
{
    public CoverPeriodExceededException() : base("The cover period exceeded.")
    {
    }
}