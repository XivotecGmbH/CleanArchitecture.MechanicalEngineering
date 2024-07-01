namespace Xivotec.CleanArchitecture.Application.Common.Process.Interfaces;

/// <summary>
/// Generic process action.
/// </summary>
public interface IProcessAction
{
    /// <summary>
    /// Executes the process action.
    /// </summary>
    /// <param name="processData">Data required for the execution of the process.</param>
    /// <param name="token">The CancellationToken.</param>
    /// <returns>A task containing resulting process data.</returns>
    public Task<ProcessDataObject> ExecuteAsync(ProcessDataObject processData, CancellationToken token);
}