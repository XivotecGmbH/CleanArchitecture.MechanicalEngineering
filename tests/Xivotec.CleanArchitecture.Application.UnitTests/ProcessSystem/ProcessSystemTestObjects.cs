using Xivotec.CleanArchitecture.Application.Processes.Common;
using Xivotec.CleanArchitecture.Application.Processes.Common.Interfaces;

namespace Xivotec.CleanArchitecture.Application.UnitTests.ProcessSystem;

internal class ProcessSystemTestObjects
{
    public readonly List<IProcessAction> EmptyTestProcessActions = [];

    public readonly List<IProcessDefinition> EmptyTestProcessDefinitions = [];

    public readonly List<IProcessAction> TestProcessActions = new()
    {
        new TestProcessAction()
    };

    public readonly List<IProcessDefinition> TestProcessDefinitions = new()
    {
        new TestProcessDefinition()
    };
}

internal class TestProcessAction : ProcessAction
{
    public override async Task<ProcessDataObject> ExecuteAsync(ProcessDataObject processData, CancellationToken token = default)
    {
        var addition = 1 + 1;

        processData.ProcessData.Add("Addition", addition);

        return await Task.FromResult(processData);
    }
}

internal class TestProcessDefinition : ProcessDefinition
{
    protected override void Initialize()
    {
        AddProcessAction<TestProcessAction>();
    }
}
