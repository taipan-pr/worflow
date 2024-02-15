using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Workflow.Application.Exceptions;
using Workflow.Domain.RequestResponse;

namespace Workflow.Api.ExceptionHandlers;

public class UserNotFoundExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;

    public UserNotFoundExceptionHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if(exception is not UserNotFoundException ex)
        {
            return false;
        }

        _logger
            .ForContext("Code", ex.ErrorCode, true)
            .ForContext("Message", ex.Message, true)
            .ForContext("Exception", exception)
            .Warning(exception.Message);

        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
        {
            ErrorCode = ex.ErrorCode,
            Message = ex.Message
        }, cancellationToken: cancellationToken);
        return true;
    }
}
