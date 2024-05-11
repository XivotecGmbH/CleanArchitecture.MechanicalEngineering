namespace Xivotec.CleanArchitecture.Presentation.Core.Messages;

public sealed record NavigationMessageDto
{
    public string Route { get; init; } = string.Empty;
    public object Value { get; init; } = new();
}
