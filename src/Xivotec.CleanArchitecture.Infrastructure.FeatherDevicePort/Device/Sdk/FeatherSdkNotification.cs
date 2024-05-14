namespace Xivotec.CleanArchitecture.Infrastructure.FeatherDevicePort.Device.Sdk;

public class FeatherSdkNotification : EventArgs
{
    public int NotificationId { get; set; }

    public string Information { get; set; } = string.Empty;

    /// <summary>
    /// True if notification contains an error
    /// </summary>
    public bool IsCritical { get; set; }
}