using MediatR;

namespace Workflow.Application.Commands.CreateDemo;

internal class CreateDemoHandler : IRequestHandler<CreateDemoRequest, CreateDemoResponse>
{
    public async Task<CreateDemoResponse> Handle(CreateDemoRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);

        return new CreateDemoResponse
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age,
            CreateDateTimeUtc = DateTime.UtcNow
        };
    }
}
