using FluentValidation;
using FluentValidation.Results;
using Workflow.Application.Exceptions;

namespace Workflow.Api.Extensions;

internal static class HttpContextAccessorExtensions
{
    public static async Task<ValidationResult> ValidateAsync<T>(this IHttpContextAccessor contextAccessor, T model, CancellationToken cancellationToken = default)
    {
        var validators = contextAccessor.HttpContext?.RequestServices.GetService<IList<IValidator<T>>>();
        if(validators is null || validators.Count == 0)
        {
            return new ValidationResult();
        }

        var failures = new List<ValidationFailure>();

        foreach (var validator in validators)
        {
            var results = await validator.ValidateAsync(model, cancellationToken);
            if(results.Errors.Count == 0) continue;

            failures.AddRange(results.Errors);
        }

        if(failures.Count != 0)
        {
            throw new ValidationFailedException(failures);
        }

        return new ValidationResult();
    }
}
