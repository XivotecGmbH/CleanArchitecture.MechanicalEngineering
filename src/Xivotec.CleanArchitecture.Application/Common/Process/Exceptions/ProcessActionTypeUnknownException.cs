namespace Xivotec.CleanArchitecture.Application.Common.Process.Exceptions;

public class ProcessActionTypeUnknownException : Exception
{

    public ProcessActionTypeUnknownException(string message, Exception exception) : base(message, exception)
    {
    }

    public ProcessActionTypeUnknownException(string message) : base(message)
    {
    }

    public ProcessActionTypeUnknownException()
    {
    }
}
