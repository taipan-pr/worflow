using Asp.Versioning;
using Carter;

namespace Workflow.Api.Endpoints;

public class DemoEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var tag = "Demo";
        var group = $"/v{{version:apiVersion}}/{tag.ToLowerInvariant()}";
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(1.1))
            .ReportApiVersions()
            .Build();

        var groupV1 = app.MapGroup(group)
            .WithTags(tag)
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(1);

        var groupV11 = app.MapGroup(group)
            .WithTags(tag)
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(1.1);

        groupV1.MapGet("/", WeatherForecastsV1);

        groupV11.MapGet("/", WeatherForecastsV2);
    }

    private static async Task<IResult> WeatherForecastsV1()
    {
        await Task.Delay(10);
        return TypedResults.Ok(new
        {
            Version = "V1"
        });
    }

    private static async Task<IResult> WeatherForecastsV2()
    {
        await Task.Delay(10);
        return TypedResults.Ok(new
        {
            Version = "V1.1"
        });
    }
}
