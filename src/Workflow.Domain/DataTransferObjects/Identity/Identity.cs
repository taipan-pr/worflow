namespace Workflow.Domain.DataTransferObjects.Identity;

public record Identity
{
    public required string Id { get; set; }
    public required string Email { get; init; }
    public bool EmailVerified { get; init; }
    public bool Disabled { get; init; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? LastSignInDateTime { get; set; }
}
