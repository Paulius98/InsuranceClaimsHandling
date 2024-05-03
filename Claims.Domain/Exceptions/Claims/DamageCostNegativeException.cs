namespace Claims.Domain.Exceptions.Claims;

public class DamageCostNegativeException : DomainException
{
    public DamageCostNegativeException() : base("Damage cost cannot be negative.")
    {
    }
}