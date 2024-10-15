using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Queries;
using Xivotec. CleanArchitecture.Application.ToDoListFeature.Dtos;

namespace Xivotec.CleanArchitecture.Presentation.Angular.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToDoItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ToDoItemDto>>> GetAll()
    {
        var listOfLists = await _mediator.Send(new GetToDoItemAllQuery());
        if (listOfLists.Count == 0)
        {
            return Ok();
        }
        return Ok(listOfLists);
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItemDto>> Create([FromBody] ToDoItemDto toDoItemDto)
    {
        await _mediator.Send(new AddToDoItemCommand(toDoItemDto));
        return Ok(toDoItemDto);
    }

    [HttpPut]
    public async Task<ActionResult<ToDoItemDto>> Update([FromBody] ToDoItemDto toDoItemDto)
    {
        await _mediator.Send(new UpdateToDoItemCommand(toDoItemDto));
        return Ok(toDoItemDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteById(Guid id)
    {
        var itemToDelete = new ToDoItemDto()
        {
            Id = id
        };

        await _mediator.Send(new DeleteToDoItemCommand(itemToDelete));
        return NoContent();
    }
}