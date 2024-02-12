using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Workflow.Api.Response;

namespace Workflow.Api.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;

    public ValidationExceptionHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if(exception is not ValidationException ex)
        {
            return false;
        }

        _logger
            .ForContext("Message", ex.Errors.Select(e => e.ErrorMessage), true)
            .ForContext("Exception", exception)
            .Warning(exception.Message);

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
        {
            Message = ex.Message,
            Errors = ex.Errors.Select(e => e.ErrorMessage)
        }, cancellationToken: cancellationToken);

        return true;
    }
}
