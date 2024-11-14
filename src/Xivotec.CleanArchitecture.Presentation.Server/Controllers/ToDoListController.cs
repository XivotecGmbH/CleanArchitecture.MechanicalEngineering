using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Dtos;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;

namespace Xivotec.CleanArchitecture.Presentation.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToDoListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ToDoListDto>>> GetAll()
    {
        var listOfLists = await _mediator.Send(new GetToDoListAllQuery());
        return Ok(listOfLists);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoListDto>> GetById(Guid id)
    {
        var list = await _mediator.Send(new GetToDoListByIdQuery(id));
        return Ok(list);
    }

    [HttpPost]
    public async Task<ActionResult<ToDoListDto>> Create([FromBody] ToDoListDto toDoListDto)
    {
        await _mediator.Send(new AddToDoListCommand(toDoListDto));
        return Ok(toDoListDto);
    }

    [HttpPut]
    public async Task<ActionResult<ToDoListDto>> Update([FromBody] ToDoListDto toDoListDto)
    {
        await _mediator.Send(new UpdateToDoListCommand(toDoListDto));
        return Ok(toDoListDto);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteById(Guid id)
    {
        var itemToDelete = new ToDoListDto
        {
            Id = id
        };

        await _mediator.Send(new DeleteToDoListCommand(itemToDelete));
        return NoContent();
    }
}
