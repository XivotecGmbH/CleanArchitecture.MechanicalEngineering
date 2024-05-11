using Xivotec.CleanArchitecture.Domain.Common;

namespace Xivotec.CleanArchitecture.Application.Common.Persistence;

/// <summary>
/// Repository item base class. Used for Repositories not containing Domain Entities.
/// </summary>
public abstract class RepositoryItem : Entity;