namespace Claims.Domain.Exceptions.Claims;

public class ClaimCreationDateInvalidException : DomainException
{
    public ClaimCreationDateInvalidException() : base("Claim creation date is invalid.")
    {
    }
}
