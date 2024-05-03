namespace Claims.Domain.Exceptions.Claims;

public class DamageCostExceededException : DomainException
{
    public DamageCostExceededException() : base("Damage cost exceeds the allowed limit.")
    {
    }
}
