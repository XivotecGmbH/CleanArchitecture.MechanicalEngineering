namespace Xivotec.CleanArchitecture.Application.Common.Process.Interfaces;

/// <summary>
/// Generic process runner.
/// </summary>
public interface IProcessRunner
{
    /// <summary>
    /// Executes a Process of type <typeparamref name="TProcessDefinitionType"/>.
    /// </summary>
    /// <typeparam name="TProcessDefinitionType">Process type to be executed</typeparam>
    /// <param name="token">Optional cancellation token</param>
    public Task ExecuteProcessDefinitionAsync<TProcessDefinitionType>(CancellationToken token = default);
}
