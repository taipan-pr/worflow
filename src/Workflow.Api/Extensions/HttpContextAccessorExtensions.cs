using Carter.ModelBinding;
using FluentValidation.Results;

namespace Workflow.Api.Extensions;

internal static class HttpContextAccessorExtensions
{
    public static ValidationResult Validate<T>(this IHttpContextAccessor contextAccessor, T model)
    {
        var result = contextAccessor.HttpContext!.Request.Validate(model);
        return result;
    }
}
