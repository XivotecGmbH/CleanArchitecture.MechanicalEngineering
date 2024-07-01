using FluentValidation;
using Xivotec.CleanArchitecture.Application.ToDoListFeature.Commands;

namespace Xivotec.CleanArchitecture.Application.ToDoListFeature.Validation.Commands;

/// <summary>
/// Validator for <see cref="UpdateToDoListCommand"/>.
/// </summary>
public sealed class UpdateToDoListCommandValidator : AbstractValidator<UpdateToDoListCommand>
{
    private const int MaxLength = 10;

    public UpdateToDoListCommandValidator()
    {
        RuleFor(x => x.Item.Id).NotEmpty();
        RuleFor(x => x.Item.Title).NotEmpty();
        RuleFor(x => x.Item.Title).MaximumLength(MaxLength);
    }
}