namespace Xivotec.CleanArchitecture.Application.Processes.Common;

public class ProcessDataObject
{
    /// <summary>
    /// Dictionary containing all relevant variables for the associated <see cref="ProcessDefinition"/>.
    /// </summary>
    public Dictionary<string, object> ProcessData { get; set; } = [];

    /// <summary>
    /// Dictionary containing all relevant conditional variables for associated <see cref="ProcessDefinition"/>.
    /// </summary>
    public Dictionary<string, bool> ProcessConditions { get; set; } = [];
}
