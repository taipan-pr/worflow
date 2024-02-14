namespace Workflow.Domain.RequestResponse.Identity;

public record RegisterRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
