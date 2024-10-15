namespace Xivotec.CleanArchitecture.Infrastructure.InfluxDbPort.Exceptions;

public class InfluxMigrationException : Exception
{
    public InfluxMigrationException()
    {
    }

    public InfluxMigrationException(string message)
        : base(message)
    {
    }

    public InfluxMigrationException(string message, Exception exception)
        : base(message, exception)
    {
    }
}