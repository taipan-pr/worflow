namespace Workflow.Application.Commands.RegisterCommand;

public record RegisterCommandResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDateTime { get; set; }
}
