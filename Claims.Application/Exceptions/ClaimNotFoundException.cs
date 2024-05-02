namespace Claims.Application.Exceptions;

public class ClaimNotFoundException : NotFoundException
{
    public ClaimNotFoundException() : base("The claim was not found.")
    {
    }
}
