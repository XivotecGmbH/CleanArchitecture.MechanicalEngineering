namespace Xivotec.CleanArchitecture.Application.Processes.Exceptions;

public class ProcessDefinitionTypeUnknownException : Exception
{

    public ProcessDefinitionTypeUnknownException(string message, Exception exception) : base(message, exception)
    {
    }

    public ProcessDefinitionTypeUnknownException(string message) : base(message)
    {
    }

    public ProcessDefinitionTypeUnknownException()
    {
    }
}
