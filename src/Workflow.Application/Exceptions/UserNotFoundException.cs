namespace Workflow.Application.Exceptions;

public class UserNotFoundException : ExceptionBase
{
    public override string ErrorCode => "USER_NOT_FOUND";
    public override string Message => "User with the given ID was not found";
}
