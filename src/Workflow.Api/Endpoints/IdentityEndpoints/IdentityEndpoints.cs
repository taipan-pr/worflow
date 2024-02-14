using System.Net;
using Asp.Versioning;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workflow.Api.Response;
using Workflow.Application.Commands.RegisterCommand;
using Workflow.Domain.RequestResponse.Identity;

namespace Workflow.Api.Endpoints.IdentityEndpoints;

public class IdentityEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var tag = "Identity";
        var group = $"/v{{version:apiVersion}}/{tag.ToLowerInvariant()}";

        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        var v1 = app.MapGroup(group)
            .WithTags(tag)
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet)
            .MapToApiVersion(1);

        v1.MapPost("/register", RegisterAsync)
            .WithSummary("Register a user")
            .WithDescription("Register user with username and password")
            .Produces<RegisterResponse>(statusCode: (int)HttpStatusCode.OK)
            .Produces<ErrorResponse>(statusCode: (int)HttpStatusCode.BadRequest);
    }

    private async Task<IResult> RegisterAsync(
        [FromBody] RegisterRequest request,
        [FromServices] IHttpContextAccessor hca,
        [FromServices] ISender mediator,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        await hca.ValidateAsync(request, cancellationToken);
        var mapRequest = mapper.Map<RegisterCommandRequest>(request);
        var response = await mediator.Send(mapRequest, cancellationToken);
        var mapResponse = mapper.Map<RegisterResponse>(response);

        return TypedResults.Ok(mapResponse);
    }
}
