namespace Claims.Application.Exceptions;

internal class CoverNotFoundException : NotFoundException
{
    public CoverNotFoundException() : base("The cover was not found.")
    {
    }
}
