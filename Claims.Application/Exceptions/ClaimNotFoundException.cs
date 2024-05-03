namespace Claims.Application.Exceptions;

public class ClaimNotFoundException : NotFoundException
{
    public ClaimNotFoundException() : base("Claim was not found.")
    {
    }
}
