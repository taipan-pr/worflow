using FluentValidation;
using FluentValidation.Results;

namespace Workflow.Application.Commands.RegisterCommand;

internal class RegisterCommandRequestValidator : AbstractValidator<RegisterCommandRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<RegisterCommandRequest> context, CancellationToken cancellation = new CancellationToken())
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(e => e.Password)
            .NotEmpty()
            .MinimumLength(6);

        return base.ValidateAsync(context, cancellation);
    }
}
