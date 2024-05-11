using FluentValidation;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Validation.Commands;

/// <summary>
/// Validator for <see cref="AddToDoListCommand"/>.
/// </summary>
public sealed class AddToDoListCommandValidator : AbstractValidator<AddToDoListCommand>
{
    public AddToDoListCommandValidator()
    {
        RuleFor(x => x.item.Id).NotEmpty();
        RuleFor(x => x.item.Title).NotEmpty();
        RuleFor(x => x.item.Title).MaximumLength(100);
    }
}