namespace Workflow.Domain.DataTransferObjects.Identity;

public record Identity
{
    public string Id { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
    public bool Disabled { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? LastSignInDateTime { get; set; }
}
