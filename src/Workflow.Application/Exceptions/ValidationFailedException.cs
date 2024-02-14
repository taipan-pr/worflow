using FluentValidation.Results;

namespace Workflow.Application.Exceptions;

public class ValidationFailedException : ExceptionBase
{
    public override string ErrorCode => "VALIDATION_FAILED";
    public override string Message => "Some values are not aligned with the validation rules";
    public IEnumerable<ValidationFailure>? Failures { get; set; }
    public IEnumerable<string>? Errors => Failures?.Select(e => e.ErrorMessage);

    public ValidationFailedException(IEnumerable<ValidationFailure> failures)
    {
        Failures = failures;
    }
}
