using MediatR;

namespace Workflow.Application.Queries.Identity.GetIdentityByIdQuery;

public record GetIdentityByIdQueryRequest : IRequest<GetIdentityByIdQueryResponse?>
{
    public string? Id { get; init; }
}
