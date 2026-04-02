using BankRecon.Application.Features.ExampleSoftDeletableEntities.Commands.Update;
using FluentValidation;

namespace BankRecon.Application.Features.ExampleSoftDeletableEntities.Validators;

public class UpdateExampleSoftDeletableEntityValidator : AbstractValidator<UpdateExampleSoftDeletableEntityCommand>
{
    public UpdateExampleSoftDeletableEntityValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}
