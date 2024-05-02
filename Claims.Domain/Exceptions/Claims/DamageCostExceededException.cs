namespace Claims.Domain.Exceptions.Claims;

public class DamageCostExceededException : DomainException
{
    public DamageCostExceededException() : base("The damage cost exceeds the allowed limit.")
    {
    }
}
