namespace Claims.Domain.Exceptions.Covers;

public class CoverStartTimeInPastException : DomainException
{
    public CoverStartTimeInPastException() : base("Start time of the cover cannot be in the past.")
    {
    }
}