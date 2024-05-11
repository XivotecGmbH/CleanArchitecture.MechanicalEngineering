using Xivotec.CleanArchitecture.Application.Processes.Common;
using Xivotec.CleanArchitecture.Application.Processes.FeatherDeviceProcess.ProcessActions;

namespace Xivotec.CleanArchitecture.Application.Processes.FeatherDeviceProcess.ProcessDefinitions;

public sealed class FeatherDeviceMachineProcess : ProcessDefinition
{
    protected override void Initialize()
    {
        ProcessDataObject.ProcessData.Add("Delay_ms", 200);

        AddProcessAction<FeatherDeviceStartMachineAction>();
        AddProcessAction<FeatherDeviceStopMachineAction>();
    }
}