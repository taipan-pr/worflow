using FluentValidation;
using FluentValidation.Results;

namespace Workflow.Application.Commands.CreateDemo;

internal class CreateDemoRequestValidator : AbstractValidator<CreateDemoRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<CreateDemoRequest> context, CancellationToken cancellation = new CancellationToken())
    {
        RuleFor(e => e.FirstName).NotEmpty();

        RuleFor(e => e.LastName).NotEmpty();

        RuleFor(e => e.Age).NotEmpty();

        return base.ValidateAsync(context, cancellation);
    }
}
