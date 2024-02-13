namespace Workflow.Domain.DataTransferObjects;

public record DemoRequest
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}
