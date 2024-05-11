namespace Xivotec.CleanArchitecture.Application.Processes.Common.Interfaces;

public interface IProcessAction
{
    public Task<ProcessDataObject> ExecuteAsync(ProcessDataObject processData, CancellationToken token);
}