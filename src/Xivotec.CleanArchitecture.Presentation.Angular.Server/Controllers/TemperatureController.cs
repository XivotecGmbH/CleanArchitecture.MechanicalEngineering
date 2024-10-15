using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Commands;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Dtos;
using Xivotec.CleanArchitecture.Application.TemperatureFeature.Queries;

namespace Xivotec.CleanArchitecture.Presentation.Angular.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemperatureController : ControllerBase
{
    private readonly IMediator _mediator;

    public TemperatureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<TemperatureEntryDto>>> GetRange([FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var temperatureEntryDtos = await _mediator.Send(new GetTemperatureEntriesRangeQuery(startDate, endDate));
        return Ok(temperatureEntryDtos);
    }

    [HttpPost]
    public async Task<ActionResult<List<TemperatureEntryDto>>> CreateBatch(
        [FromBody] List<TemperatureEntryDto> temperatureEntryDtos)
    {
        await _mediator.Send(new AddTemperatureEntriesCommand(temperatureEntryDtos));
        return Ok(temperatureEntryDtos);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        await _mediator.Send(new DeleteTemperatureEntryRangeCommand(startDate, endDate));
        return NoContent();
    }
}