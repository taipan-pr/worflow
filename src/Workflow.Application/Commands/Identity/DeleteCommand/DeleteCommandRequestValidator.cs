using FluentValidation;
using FluentValidation.Results;

namespace Workflow.Application.Commands.Identity.DeleteCommand;

public class DeleteCommandRequestValidator : AbstractValidator<DeleteCommandRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DeleteCommandRequest> context, CancellationToken cancellation = new CancellationToken())
    {
        RuleFor(e => e.Id).NotEmpty();

        return base.ValidateAsync(context, cancellation);
    }
}
