using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Workflow.Domain.RequestResponse;

namespace Workflow.Api.ExceptionHandlers;

internal class UnhandledExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;

    public UnhandledExceptionHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.Error(exception, exception.Message);

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
        {
            ErrorCode = "UNHANDLED",
            Message = "Something went wrong on our side"
        }, cancellationToken: cancellationToken);

        return true;
    }
}
