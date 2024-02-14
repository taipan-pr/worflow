namespace Workflow.Application.Commands.Identity.RegisterCommand;

public record RegisterCommandResponse
{
    public required string Id { get; init; }
    public required string Email { get; init; }
    public DateTime CreatedDateTime { get; init; }
}
