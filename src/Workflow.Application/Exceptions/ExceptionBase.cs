namespace Workflow.Application.Exceptions;

public abstract class ExceptionBase : Exception
{
    public abstract string ErrorCode { get; }
    public new abstract string Message { get; }
}
