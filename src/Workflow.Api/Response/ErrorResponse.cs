namespace Workflow.Api.Response;

public class ErrorResponse
{
    public string Message { get; init; } = string.Empty;
    public IEnumerable<string> Errors { get; init; } = new List<string>();
}