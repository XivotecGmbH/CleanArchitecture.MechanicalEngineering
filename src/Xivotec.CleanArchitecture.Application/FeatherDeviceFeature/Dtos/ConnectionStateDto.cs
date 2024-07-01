using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Enums;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

/// <summary>
/// DTO representing a <see cref="ConnectionState"/>
/// </summary>
public enum ConnectionStateDto
{
    Disconnected,
    Connected
}