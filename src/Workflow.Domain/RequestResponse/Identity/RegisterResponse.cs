namespace Workflow.Domain.RequestResponse.Identity;

public record RegisterResponse
{
    public required string Id { get; init; }
    public required string Email { get; init; }
    public DateTime CreatedDateTime { get; init; }
}
