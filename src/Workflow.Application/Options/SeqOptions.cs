namespace Workflow.Application.Options;

public record SeqOptions
{
    public required string Host { get; init; }
    public required string ApiKey { get; init; }
}
