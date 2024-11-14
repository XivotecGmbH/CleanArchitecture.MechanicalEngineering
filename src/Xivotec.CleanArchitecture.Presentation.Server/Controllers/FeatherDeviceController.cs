using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Queries;

namespace Xivotec.CleanArchitecture.Presentation.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeatherDeviceController : ControllerBase
{
    private readonly IMediator _mediator;

    public FeatherDeviceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<FeatherDeviceDto>>> GetAll()
    {
        var featherDeviceDtos = await _mediator.Send(new GetFeatherDeviceAllQuery());
        return Ok(featherDeviceDtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FeatherDeviceDto>> GetById(Guid id)
    {
        var featherDeviceDto = await _mediator.Send(new GetFeatherDeviceByIdQuery(id));
        return Ok(featherDeviceDto);
    }

    [HttpGet("{id:guid}/state")]
    public async Task<ActionResult<FeatherDeviceDto>> GetConnectionStateById(Guid id)
    {
        var featherDeviceDto = await _mediator.Send(new GetFeatherDeviceByIdQuery(id));
        return Ok(featherDeviceDto);
    }

    [HttpGet("{id:guid}/recipe")]
    public async Task<ActionResult<FeatherDeviceDto>> GetDeviceRecipeById(Guid id)
    {
        var featherDeviceDto = await _mediator.Send(new GetFeatherDeviceByIdQuery(id));
        return Ok(featherDeviceDto);
    }

    [HttpPost]
    public async Task<ActionResult<FeatherDeviceDto>> Create([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        var createdFeatherDeviceDto = await _mediator.Send(new AddFeatherDeviceCommand(featherDeviceDto));
        return Ok(createdFeatherDeviceDto);
    }

    [HttpPut]
    public async Task<ActionResult<FeatherDeviceDto>> Update([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new UpdateFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<FeatherDeviceDto>> DeleteById(Guid id)
    {
        var featherDeviceDto = new FeatherDeviceDto
        {
            Id = id
        };
        await _mediator.Send(new DeleteFeatherDeviceCommand(featherDeviceDto));
        return NoContent();
    }

    [HttpPost("actions/connect")]
    public async Task<ActionResult<FeatherDeviceDto>> Connect([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new ConnectFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/disconnect")]
    public async Task<ActionResult<FeatherDeviceDto>> Disconnect([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new DisconnectFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/initialize")]
    public async Task<ActionResult<FeatherDeviceDto>> Initialize([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new InitializeFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/deinitialize")]
    public async Task<ActionResult<FeatherDeviceDto>> DeInitialize([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new DeinitializeFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/start")]
    public async Task<ActionResult<FeatherDeviceDto>> Start([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new StartFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/stop")]
    public async Task<ActionResult<FeatherDeviceDto>> Stop([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new StopFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/pause")]
    public async Task<ActionResult<FeatherDeviceDto>> Pause([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new PauseFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/continue")]
    public async Task<ActionResult<FeatherDeviceDto>> Continue([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new ContinueFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/load")]
    public async Task<ActionResult<FeatherDeviceDto>> LoadRecipe([FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new LoadFeatherDeviceRecipeCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/distance")]
    public async Task<ActionResult<FeatherDeviceDto>> StartDistanceDataReceiving(
        [FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new StartDistanceDataReceivingFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/lumen")]
    public async Task<ActionResult<FeatherDeviceDto>> StartLumenDataReceiving(
        [FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new StartLumenDataReceivingFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }

    [HttpPost("actions/temperature")]
    public async Task<ActionResult<FeatherDeviceDto>> StartTemperatureDataReceiving(
        [FromBody] FeatherDeviceDto featherDeviceDto)
    {
        await _mediator.Send(new StartTemperatureDataReceivingFeatherDeviceCommand(featherDeviceDto));
        return Ok(featherDeviceDto);
    }
}