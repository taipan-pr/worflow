using FluentValidation;
using FluentValidation.Results;
using Workflow.Domain.RequestResponse.Identity;

namespace Workflow.Api.Endpoints.IdentityEndpoints.Validators;

internal class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<RegisterRequest> context, CancellationToken cancellation = new CancellationToken())
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
