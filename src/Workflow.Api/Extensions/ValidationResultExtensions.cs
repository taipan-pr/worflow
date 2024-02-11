using FluentValidation.Results;
using Workflow.Api.Response;

namespace Workflow.Api.Extensions;

public static class ValidationResultExtensions
{
    public static ErrorResponse ToErrorResponse(this ValidationResult result)
    {
        return new ErrorResponse
        {
            Message = "Validation error",
            Errors = result.Errors.Select(e => e.ErrorMessage)
        };
    }
}
