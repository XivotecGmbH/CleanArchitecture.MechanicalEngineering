namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Exceptions;

public class FeatherDeviceActionNotFoundException : Exception
{
    public FeatherDeviceActionNotFoundException()
    {
    }

    public FeatherDeviceActionNotFoundException(string message)
        : base(message)
    {
    }

    public FeatherDeviceActionNotFoundException(string message, Exception exception)
        : base(message, exception)
    {
    }
}