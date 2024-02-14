using System.Net;
using Asp.Versioning;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workflow.Api.Response;
using Workflow.Application.Commands.CreateDemo;
using Workflow.Application.Queries.GetWeatherForecastQuery;
using Workflow.Domain.RequestResponse.WeatherForecast;

namespace Workflow.Api.Endpoints.WeatherForecastEndpoints;

public class WeatherForecastEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        var tag = "WeatherForecast";
        var group = $"/v{{version:apiVersion}}/{tag.ToLowerInvariant()}";
        var groupV1 = app.MapGroup(group)
            .WithTags(tag)
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(1);

        var groupV2 = app.MapGroup(group)
            .WithTags(tag)
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(2);

        groupV1.MapGet("/", WeatherForecasts);

        groupV1.MapPost("/demo", DemoHandler)
            .WithSummary("Hello Summary")
            .WithDescription("Hello description")
            .Produces<DemoResponse>(statusCode: (int)HttpStatusCode.OK)
            .Produces<ErrorResponse>(statusCode: (int)HttpStatusCode.BadRequest);

        groupV2.MapPost("/demo", DemoHandler)
            .WithSummary("Hello Summary")
            .WithDescription("Hello description")
            .Produces<DemoResponse>(statusCode: (int)HttpStatusCode.OK)
            .Produces<ErrorResponse>(statusCode: (int)HttpStatusCode.BadRequest);
    }

    private static async Task<IResult> DemoHandler(
        [FromBody] DemoRequest request,
        [FromServices] IHttpContextAccessor hca,
        [FromServices] ISender mediator,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        await hca.ValidateAsync(request, cancellationToken);
        var mapRequest = mapper.Map<CreateDemoRequest>(request, options =>
        {
            options.AfterMap((_, demoRequest) =>
            {
                demoRequest.Age = 10;
            });
        });
        var response = await mediator.Send(mapRequest, cancellationToken);
        var mapResponse = mapper.Map<DemoResponse>(response);

        return TypedResults.Ok(mapResponse);
    }

    private static async Task<IEnumerable<GetWeatherForecastResponse>> WeatherForecasts(
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetWeatherForecastRequest(), cancellationToken);
        return result;
    }
}
