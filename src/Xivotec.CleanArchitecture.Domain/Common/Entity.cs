namespace Xivotec.CleanArchitecture.Domain.Common;

/// <summary>
/// Entity base class.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Unique Entity Id
    /// </summary>
    public Guid Id { get; init; }
}
