using FluentValidation;
using FluentValidation.Results;
using Workflow.Domain.DataTransferObjects;

namespace Workflow.Api.Endpoints.WeatherForecastEndpoints.Validators;

internal class DemoRequestValidator : AbstractValidator<DemoRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<DemoRequest> context, CancellationToken cancellation = new CancellationToken())
    {
        RuleFor(e => e.FirstName).NotEmpty();

        RuleFor(e => e.LastName).NotEmpty();

        return base.ValidateAsync(context, cancellation);
    }
}
