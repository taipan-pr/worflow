namespace Workflow.Domain.DataTransferObjects;

public record DemoResponse
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required int Age { get; init; }
    public required DateTime CreateDateTimeUtc { get; init; }
}
