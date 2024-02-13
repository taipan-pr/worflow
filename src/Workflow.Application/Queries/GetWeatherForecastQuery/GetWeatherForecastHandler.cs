using MediatR;
using Serilog;

namespace Workflow.Application.Queries.GetWeatherForecastQuery;

internal class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecastRequest, IEnumerable<GetWeatherForecastResponse>>
{
    private readonly ILogger _logger;

    private readonly List<string> _summaries =
    [
        "Freezing",
        "Bracing",
        "Chilly",
        "Cool",
        "Mild",
        "Warm",
        "Balmy",
        "Hot",
        "Sweltering",
        "Scorching"
    ];

    public GetWeatherForecastHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<GetWeatherForecastResponse>> Handle(GetWeatherForecastRequest request, CancellationToken cancellationToken)
    {
        _logger.Information("Hello from {Handler}", nameof(GetWeatherForecastHandler));
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new GetWeatherForecastResponse
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = _summaries[Random.Shared.Next(_summaries.Count)]
            }
        );
        await Task.Delay(10, cancellationToken);
        return forecast;
    }
}
