using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Workflow.Application.Exceptions;
using Workflow.Domain.RequestResponse;

namespace Workflow.Api.ExceptionHandlers;

internal class ValidationExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;

    public ValidationExceptionHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if(exception is not ValidationFailedException ex)
        {
            return false;
        }

        _logger
            .ForContext("Code", ex.ErrorCode, true)
            .ForContext("Message", ex.Message, true)
            .ForContext("Failures", ex.Errors, true)
            .ForContext("Exception", exception)
            .Warning(exception.Message);

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
        {
            ErrorCode = ex.ErrorCode,
            Message = ex.Message,
            Errors = ex.Errors
        }, cancellationToken: cancellationToken);

        return true;
    }
}
