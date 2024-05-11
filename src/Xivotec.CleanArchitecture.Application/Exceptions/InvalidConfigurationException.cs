namespace Xivotec.CleanArchitecture.Application.Exceptions;

public sealed class InvalidConfigurationException : Exception
{
    public InvalidConfigurationException()
    {
    }

    public InvalidConfigurationException(string configurationName)
        : base($"Invalid or incomplete configuration: {configurationName}")
    {
    }

    public InvalidConfigurationException(string configurationName, Exception exception)
        : base($"Invalid or incomplete configuration: {configurationName}", exception)
    {
    }
}