using System.Collections.ObjectModel;

namespace Xivotec.CleanArchitecture.Application.Common.Process.Interfaces;

/// <summary>
/// Generic process definition.
/// </summary>
public interface IProcessDefinition
{
    /// <summary>
    /// Internal List of all <see cref="ProcessAction"/> the process should
    /// run. Defined as a list of types.
    /// </summary>
    public ReadOnlyCollection<Type> ProcessActions { get; }

    /// <summary>
    /// DataObject the process uses to track specific
    /// values and conditions through its execution. 
    /// </summary>
    public ProcessDataObject ProcessDataObject { get; set; }

    /// <summary>
    /// Adds a new <see cref="IProcessAction"/> defined by type to the process
    /// pipeline definition.
    /// </summary>
    /// <typeparam name="TProcessAction"><see cref="IProcessAction"/> type to be
    /// added to the <see cref="IProcessDefinition"/> pipeline</typeparam>
    public void AddProcessAction<TProcessAction>();
}