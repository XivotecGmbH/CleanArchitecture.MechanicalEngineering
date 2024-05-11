using Xivotec.CleanArchitecture.Application.Processes.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Application.Processes.Common;

public abstract class ProcessAction : IProcessAction
{
    public abstract Task<ProcessDataObject> ExecuteAsync(ProcessDataObject processData, CancellationToken token);
}
