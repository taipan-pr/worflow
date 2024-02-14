namespace Workflow.Application.Exceptions;

public class UserExistsException : ExceptionBase
{
    public override string ErrorCode => "EMAIL_EXISTS";
    public override string Message => "The user with the provided email already exists";
}
