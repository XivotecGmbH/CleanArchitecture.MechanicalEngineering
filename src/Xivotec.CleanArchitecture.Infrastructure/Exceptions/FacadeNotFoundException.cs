namespace Xivotec.CleanArchitecture.Infrastructure.Exceptions;

public sealed class FacadeNotFoundException : Exception
{
    public FacadeNotFoundException()
    {
    }

    public FacadeNotFoundException(string message)
        : base(message)
    {
    }

    public FacadeNotFoundException(string message, Exception exception)
        : base(message, exception)
    {
    }
}
