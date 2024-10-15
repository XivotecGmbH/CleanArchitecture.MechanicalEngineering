using Microsoft.AspNetCore.Mvc;
using Xivotec.CleanArchitecture.Application.Common.Process.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherProcessFeature.ProcessDefinitions;

namespace Xivotec.CleanArchitecture.Presentation.Angular.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProcessController : ControllerBase
{
    private readonly IProcessRunner _processRunner;

    public ProcessController(IProcessRunner processRunner)
    {
        _processRunner = processRunner;
    }

    [HttpPost]
    public async Task<ActionResult> PostProcessStart()
    {
        await _processRunner.ExecuteProcessDefinitionAsync<FeatherDeviceMachineProcess>();
        return Ok();
    }
}