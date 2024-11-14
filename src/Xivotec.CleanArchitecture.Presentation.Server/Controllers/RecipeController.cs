using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xivotec.CleanArchitecture.Application.Common.Recipe;
using Xivotec.CleanArchitecture.Application.Common.Recipe.Interface;
using Xivotec.CleanArchitecture.Application.FeatherRecipeFeature.Queries;

namespace Xivotec.CleanArchitecture.Presentation.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IRecipeService _recipeService;

    public RecipeController(IMediator mediator, IRecipeService recipeService)
    {
        _mediator = mediator;
        _recipeService = recipeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<XivotecRecipeDto>>> GetAll()
    {
        var xivotecRecipeDtos = await _mediator.Send(new GetXivotecRecipeAllQuery());
        return Ok(xivotecRecipeDtos);
    }

    [HttpPost]
    public async Task<ActionResult<XivotecRecipeDto>> Create([FromBody] XivotecRecipeDto xivotecRecipeDto)
    {
        await _recipeService.SaveRecipeAsync(xivotecRecipeDto);
        return Ok(xivotecRecipeDto);
    }

    [HttpPut]
    public async Task<ActionResult<XivotecRecipeDto>> Update([FromBody] XivotecRecipeDto xivotecRecipeDto)
    {
        await _recipeService.UpdateRecipeAsync(xivotecRecipeDto);
        return Ok(xivotecRecipeDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteById(Guid id)
    {
        var recipe = await _mediator.Send(new GetXivotecRecipeByIdQuery(id));

        await _recipeService.DeleteRecipeAsync(recipe);
        return NoContent();
    }
}