using Xivotec.CleanArchitecture.Application.Common.Process;
using Xivotec.CleanArchitecture.Application.FeatherProcessFeature.ProcessActions;

namespace Xivotec.CleanArchitecture.Application.FeatherProcessFeature.ProcessDefinitions;

public sealed class FeatherDeviceMachineProcess : ProcessDefinition
{
    protected override void Initialize()
    {
        ProcessDataObject.ProcessData.Add("Delay_ms", 200);

        AddProcessAction<FeatherDeviceInitMachineAction>();
        AddProcessAction<FeatherDeviceStartMachineAction>();
        AddProcessAction<FeatherDeviceStopMachineAction>();
    }
}