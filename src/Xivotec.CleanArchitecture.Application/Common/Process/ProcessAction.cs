using Xivotec.CleanArchitecture.Application.Common.Process.Interfaces;

namespace Xivotec.CleanArchitecture.Application.Common.Process;

///<inheritdoc cref="IProcessAction"/>
public abstract class ProcessAction : IProcessAction
{
    public abstract Task<ProcessDataObject> ExecuteAsync(ProcessDataObject processData, CancellationToken token);
}
