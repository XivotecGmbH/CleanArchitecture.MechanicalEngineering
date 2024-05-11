using System.Collections.ObjectModel;

namespace Xivotec.CleanArchitecture.Application.Processes.Common.Interfaces;

public interface IProcessDefinition
{
    /// <summary>
    /// Internal List of all <see cref="ProcessAction"/> the Process should
    /// run. Defined as a list of Types.
    /// </summary>
    public ReadOnlyCollection<Type> ProcessActions { get; }

    /// <summary>
    /// DataObject the Process uses to track specific
    /// values and conditions through its execution. 
    /// </summary>
    public ProcessDataObject ProcessDataObject { get; set; }

    /// <summary>
    /// Adds a new <see cref="IProcessAction"/> defined by type to the process
    /// pipeline definition.
    /// </summary>
    /// <typeparam name="TProcessAction"><see cref="IProcessAction"/> Typ to be
    /// added to the <see cref="IProcessDefinition"/> pipeline</typeparam>
    public void AddProcessAction<TProcessAction>();
}