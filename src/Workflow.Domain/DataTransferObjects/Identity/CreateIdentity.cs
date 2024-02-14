namespace Workflow.Domain.DataTransferObjects.Identity;

public record CreateIdentity
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
