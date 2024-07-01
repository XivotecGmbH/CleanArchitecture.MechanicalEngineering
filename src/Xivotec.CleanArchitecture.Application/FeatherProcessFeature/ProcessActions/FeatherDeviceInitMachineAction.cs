using Microsoft.Extensions.Logging;
using Xivotec.CleanArchitecture.Application.Common.Device.Interface;
using Xivotec.CleanArchitecture.Application.Common.Process;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;

namespace Xivotec.CleanArchitecture.Application.FeatherProcessFeature.ProcessActions;

public class FeatherDeviceInitMachineAction : ProcessAction
{
    private readonly ILogger<FeatherDeviceInitMachineAction> _logger;

    // Only for demonstration purposes for now. Later real Use Case
    private readonly IDeviceService<FeatherDeviceDto> _deviceService;

    public FeatherDeviceInitMachineAction(
        ILogger<FeatherDeviceInitMachineAction> logger,
        IDeviceUnitOfWork deviceUnitOfWork
        )
    {
        _logger = logger;
        _deviceService = deviceUnitOfWork.GetDeviceService<FeatherDeviceDto>();
    }

    public override async Task<ProcessDataObject> ExecuteAsync(ProcessDataObject processData,
        CancellationToken token)
    {
        _logger.LogInformation("Init");
        return await Task.FromResult(processData);
    }
}