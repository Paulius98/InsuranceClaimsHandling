namespace Claims.Domain.Exceptions.Covers;

public class CoverStartTimeInPastException : DomainException
{
    public CoverStartTimeInPastException() : base("The start time of the cover cannot be in the past.")
    {
    }
}