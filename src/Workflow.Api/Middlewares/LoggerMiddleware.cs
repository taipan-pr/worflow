using System.Text;

namespace Workflow.Api.Middlewares;

internal class LoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public LoggerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await using var request = new MemoryStream();
        var requestJson = await GetRequestBodyAsync(context, request);
        await using var response = new MemoryStream();
        var originalBody = context.Response.Body;
        context.Response.Body = response;
        await _next(context);
        response.Seek(0, SeekOrigin.Begin);
        var responseJson = await new StreamReader(context.Response.Body).ReadToEndAsync();
        response.Seek(0, SeekOrigin.Begin);
        await response.CopyToAsync(originalBody);

        var logger = _logger
            .ForContext("Request", requestJson, true)
            .ForContext("Response", responseJson, true);
        switch (context.Response.StatusCode)
        {
            case >= 200 and < 300:
                logger.Information("Request & Response Logger");
                break;
            case >= 400 and < 500:
                logger.Warning("Request & Response Logger");
                break;
            case >= 500:
                logger.Error("Request & Response Logger");
                break;
        }
    }

    private static async Task<string> GetRequestBodyAsync(HttpContext context, Stream stream)
    {
        if((!string.IsNullOrWhiteSpace(context.Request.ContentType) &&
            context.Request.ContentType.Contains("multipart/form-data")) ||
           context.Request.ContentLength is null or 0) return string.Empty;
        using var bodyReader = new StreamReader(context.Request.Body);
        var bodyAsText = await bodyReader.ReadToEndAsync();
        var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
        await stream.WriteAsync(bytesToWrite);
        stream.Seek(0, SeekOrigin.Begin);
        context.Request.Body = stream;
        return bodyAsText.Replace(" ", string.Empty)
            .Replace("\r", string.Empty)
            .Replace("\n", string.Empty);
    }
}
