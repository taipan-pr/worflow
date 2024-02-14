namespace Workflow.Domain.RequestResponse.WeatherForecast;

public record DemoRequest
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}
