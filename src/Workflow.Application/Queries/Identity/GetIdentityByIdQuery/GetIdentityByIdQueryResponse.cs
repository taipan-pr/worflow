namespace Workflow.Application.Queries.Identity.GetIdentityByIdQuery;

public record GetIdentityByIdQueryResponse
{
    public required string Id { get; init; }
    public required string Email { get; init; }
    public bool EmailVerified { get; init; }
    public bool Disabled { get; init; }
    public DateTime? CreatedDateTime { get; init; }
    public DateTime? LastSignInDateTime { get; init; }
}
