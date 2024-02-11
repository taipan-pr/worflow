using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Workflow.Application.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var failures = new List<ValidationFailure>();
        foreach (var validator in _validators)
        {
            var results = await validator.ValidateAsync(request, cancellationToken);
            if(results.Errors.Count == 0) continue;

            failures.AddRange(results.Errors);
        }

        if(failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
