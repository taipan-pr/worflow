namespace Workflow.Domain.RequestResponse.Identity;

public record RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
