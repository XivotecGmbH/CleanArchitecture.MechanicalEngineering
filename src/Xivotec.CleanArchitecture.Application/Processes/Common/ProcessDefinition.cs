using System.Collections.ObjectModel;
using Xivotec.CleanArchitecture.Application.Processes.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Application.Processes.Common;

public abstract class ProcessDefinition : IProcessDefinition
{
    public ReadOnlyCollection<Type> ProcessActions
        => _processActions.AsReadOnly();

    public ProcessDataObject ProcessDataObject { get; set; } = new();

    private readonly List<Type> _processActions = [];

    protected ProcessDefinition()
    {
        Initialize();
    }

    /// <summary>
    /// Add a <see cref="ProcessAction"/> of Type <typeparamref name="TProcessAction"/> to the Process.
    /// </summary>
    /// <typeparam name="TProcessAction"><see cref="ProcessAction"/> Type to be added</typeparam>
    public void AddProcessAction<TProcessAction>()
        => _processActions.Add(typeof(TProcessAction));

    protected abstract void Initialize();
}