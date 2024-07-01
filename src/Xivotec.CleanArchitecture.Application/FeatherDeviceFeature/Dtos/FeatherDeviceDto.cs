using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

/// <summary>
/// DTO representing a <see cref="FeatherDevice"/>.
/// </summary>
public record FeatherDeviceDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ComPort { get; set; } = string.Empty;

    public ConnectionStateDto ConnectionState { get; set; } = ConnectionStateDto.Disconnected;

    public string ConnectionString { get; set; } = string.Empty;

    public List<LedColorDto> AvailableLedColors { get; set; } = [];

    public FeatherDeviceActionsDto Action { get; set; } = FeatherDeviceActionsDto.None;

    public XivotecRecipeDto Recipe { get; set; } = new();
}
