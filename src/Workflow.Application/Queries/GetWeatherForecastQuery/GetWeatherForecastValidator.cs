using FluentValidation;
using FluentValidation.Results;

namespace Workflow.Application.Queries.GetWeatherForecastQuery;

internal class GetWeatherForecastValidator : AbstractValidator<GetWeatherForecastRequest>
{
    public override Task<ValidationResult> ValidateAsync(ValidationContext<GetWeatherForecastRequest> context, CancellationToken cancellation = new CancellationToken())
    {
        return base.ValidateAsync(context, cancellation);
    }
}
