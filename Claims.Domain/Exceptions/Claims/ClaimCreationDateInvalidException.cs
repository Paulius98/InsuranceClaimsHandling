namespace Claims.Domain.Exceptions.Claims;

public class ClaimCreationDateInvalidException : DomainException
{
    public ClaimCreationDateInvalidException() : base("The claim creation date is invalid.")
    {
    }
}
