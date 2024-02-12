using System.Net;
using Asp.Versioning;
using Carter;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workflow.Api.Extensions;
using Workflow.Api.Response;
using Workflow.Application.Queries.GetWeatherForecastQuery;

namespace Workflow.Api.Endpoints;

public class WeatherForecastEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        var groupV1 = app.MapGroup("/v{version:apiVersion}/weatherforecast")
            .WithTags("WeatherForecast")
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(1);

        var groupV2 = app.MapGroup("/v{version:apiVersion}/weatherforecast")
            .WithTags("WeatherForecast")
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(2);

        groupV1.MapGet("/", WeatherForecasts);

        groupV1.MapPost("/demo", DemoHandler)
            .WithSummary("Hello Summary")
            .WithDescription("Hello description")
            .Produces<Person>(statusCode: (int)HttpStatusCode.OK)
            .Produces<ErrorResponse>(statusCode: (int)HttpStatusCode.BadRequest);

        groupV2.MapPost("/demo", DemoHandler)
            .WithSummary("Hello Summary")
            .WithDescription("Hello description")
            .Produces<Person>(statusCode: (int)HttpStatusCode.OK)
            .Produces<ErrorResponse>(statusCode: (int)HttpStatusCode.BadRequest);
    }

    private static async Task<IResult> DemoHandler(
        [FromServices] IHttpContextAccessor hca,
        [FromBody] Person id)
    {
        await Task.Delay(100);
        var result = hca.Validate(id);
        if(!result.IsValid)
        {
            throw new ValidationException("Validation Failed", result.Errors);
        }

        return TypedResults.Ok(id);
    }

    private static async Task<IEnumerable<GetWeatherForecastResponse>> WeatherForecasts(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetWeatherForecastRequest(), cancellationToken);
        return result;
    }
}

internal record Person(string FirstName, string LastName);

internal class PersonValidator : AbstractValidator<Person>
{
    public override ValidationResult Validate(ValidationContext<Person> context)
    {
        RuleFor(e => e.FirstName).NotEmpty();

        RuleFor(e => e.LastName).NotEmpty();

        return base.Validate(context);
    }
}
