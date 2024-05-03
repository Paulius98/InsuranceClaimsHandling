namespace Claims.Application.Exceptions;

internal class CoverNotFoundException : NotFoundException
{
    public CoverNotFoundException() : base("Cover was not found.")
    {
    }
}
