using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Workflow.Api.Response;

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
            Message = "Something went wrong"
        }, cancellationToken: cancellationToken);

        return true;
    }
}
