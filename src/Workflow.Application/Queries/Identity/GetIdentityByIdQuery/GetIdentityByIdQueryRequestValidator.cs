using FluentValidation;
using FluentValidation.Results;

namespace Workflow.Application.Queries.Identity.GetIdentityByIdQuery;

internal class GetIdentityByIdQueryRequestValidator : AbstractValidator<GetIdentityByIdQueryRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetIdentityByIdQueryRequest> context, CancellationToken cancellation = new CancellationToken())
    {
        RuleFor(e => e.Id).NotEmpty();

        return base.ValidateAsync(context, cancellation);
    }
}
