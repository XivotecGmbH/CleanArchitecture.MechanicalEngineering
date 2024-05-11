namespace Xivotec.CleanArchitecture.Application.Processes.Common.Interfaces;

public interface IProcessRunner
{
    public Task ExecuteProcessDefinitionAsync<TProcessDefinitionType>(CancellationToken token = default);
}
