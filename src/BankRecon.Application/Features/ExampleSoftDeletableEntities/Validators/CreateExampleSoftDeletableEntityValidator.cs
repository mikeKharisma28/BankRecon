using BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Create;
using FluentValidation;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Validators;

public class CreateExampleSoftDeletableEntityValidator : AbstractValidator<CreateExampleSoftDeletableEntityCommand>
{
    public CreateExampleSoftDeletableEntityValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}
