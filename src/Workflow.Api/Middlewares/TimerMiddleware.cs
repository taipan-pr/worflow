using System.Diagnostics;

namespace Workflow.Api.Middlewares;

public class TimerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public TimerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.Information("Start Request: {Path}", context.Request.Path);
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();
        switch (context.Response.StatusCode)
        {
            case >= 200 and < 300:
                _logger.Information("Status: {Status}, Finished Request: {Path}, Elapsed: {Elapsed}", context.Response.StatusCode, context.Request.Path, stopwatch.Elapsed);
                break;
            case >= 400 and < 500:
                _logger.Warning("Status: {Status}, Finished Request: {Path}, Elapsed: {Elapsed}", context.Response.StatusCode, context.Request.Path, stopwatch.Elapsed);
                break;
            case >= 500:
                _logger.Error("Status: {Status}, Finished Request: {Path}, Elapsed: {Elapsed}", context.Response.StatusCode, context.Request.Path, stopwatch.Elapsed);
                break;
        }
    }
}
