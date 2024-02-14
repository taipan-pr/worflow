namespace Workflow.Domain.RequestResponse;

public class ErrorResponse
{
    public required string ErrorCode { get; init; }
    public string? Message { get; init; }
    public IEnumerable<string>? Errors { get; init; }
}
