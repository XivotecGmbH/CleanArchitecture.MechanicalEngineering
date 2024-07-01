namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

/// <summary>
/// DTO for controlling feather device actions./>
/// </summary>
public enum FeatherDeviceActionsDto
{
    None,
    Start,
    Stop,
    Pause,
    Continue,
    StartTemperatureDataReceiving,
    StartDistanceDataReceiving,
    StartLumenDataReceiving,
    GetConnectionState
}