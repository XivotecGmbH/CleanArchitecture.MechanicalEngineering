namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Sdk;

public class FeatherSdkStreamData : EventArgs
{
    public int Id { get; set; }

    public string Data { get; set; } = string.Empty;
}