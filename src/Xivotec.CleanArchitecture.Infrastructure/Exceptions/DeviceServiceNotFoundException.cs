namespace Xivotec.CleanArchitecture.Infrastructure.Exceptions;

public class DeviceServiceNotFoundException : Exception
{
    public DeviceServiceNotFoundException()
    {
    }

    public DeviceServiceNotFoundException(string message)
        : base(message)
    {
    }

    public DeviceServiceNotFoundException(string message, Exception exception)
        : base(message, exception)
    {
    }
}