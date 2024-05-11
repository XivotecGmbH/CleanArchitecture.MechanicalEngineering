﻿using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Application.Processes.Common;
using Xivotec.CleanArchitecture.Application.Services.Device;

namespace Xivotec.CleanArchitecture.Application.Processes.FeatherDeviceProcess.ProcessActions;

public class FeatherDeviceStopMachineAction : ProcessAction
{
    private readonly ILogger<FeatherDeviceStartMachineAction> _logger;

    // Only for demonstration pupose for now. Later real Use Case
    private readonly IFeatherDeviceService _deviceService;

    public FeatherDeviceStopMachineAction(
        ILogger<FeatherDeviceStartMachineAction> logger,
        IFeatherDeviceService deviceService
        )
    {
        _logger = logger;
        _deviceService = deviceService;
    }

    public override async Task<ProcessDataObject> ExecuteAsync(ProcessDataObject processData, CancellationToken token)
    {
        _logger.LogInformation("Stop");
        return await Task.FromResult(processData);
    }
}