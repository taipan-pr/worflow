using System.Net;
using Asp.Versioning;
using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Commands.Identity.DeleteCommand;
using Workflow.Application.Commands.Identity.RegisterCommand;
using Workflow.Application.Queries.Identity.GetIdentityByIdQuery;
using Workflow.Domain.RequestResponse;
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

        v1.MapGet("/{id}", FindByIdAsync)
            .WithSummary("Find user by ID")
            .WithDescription("Find user with the ID")
            .Produces<IdentityResponse>(statusCode: (int)HttpStatusCode.OK)
            .Produces(statusCode: (int)HttpStatusCode.NotFound)
            .Produces<ErrorResponse>(statusCode: (int)HttpStatusCode.BadRequest);

        v1.MapDelete("/{id}", DeleteAsync)
            .WithSummary("Delete user by ID")
            .WithDescription("Delete user with the ID")
            .Produces(statusCode: (int)HttpStatusCode.OK)
            .Produces(statusCode: (int)HttpStatusCode.NotFound)
            .Produces<ErrorResponse>(statusCode: (int)HttpStatusCode.BadRequest);
    }

    private static async Task<IResult> DeleteAsync(
        [FromRoute] string id,
        [FromServices] ISender sender,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var request = new DeleteCommandRequest
        {
            Id = id
        };
        await sender.Send(request, cancellationToken);
        return TypedResults.Ok();
    }

    private static async Task<IResult> FindByIdAsync(
        [FromRoute] string id,
        [FromServices] ISender sender,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        var request = new GetIdentityByIdQueryRequest
        {
            Id = id
        };
        var response = await sender.Send(request, cancellationToken);
        var mapResponse = mapper.Map<IdentityResponse>(response);
        return response is null ? TypedResults.NotFound() : TypedResults.Ok(mapResponse);
    }

    private static async Task<IResult> RegisterAsync(
        [FromBody] RegisterRequest request,
        [FromServices] IHttpContextAccessor hca,
        [FromServices] ISender sender,
        [FromServices] IMapper mapper,
        CancellationToken cancellationToken)
    {
        await hca.ValidateAsync(request, cancellationToken);
        var mapRequest = mapper.Map<RegisterCommandRequest>(request);
        var response = await sender.Send(mapRequest, cancellationToken);
        var mapResponse = mapper.Map<RegisterResponse>(response);

        return TypedResults.Ok(mapResponse);
    }
}
