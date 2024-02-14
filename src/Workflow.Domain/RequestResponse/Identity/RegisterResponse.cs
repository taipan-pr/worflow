namespace Workflow.Domain.RequestResponse.Identity;

public record RegisterResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDateTime { get; set; }
}
