using MediatR;

namespace Workflow.Application.Commands.CreateDemo;

public class CreateDemoRequest : IRequest<CreateDemoResponse>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Age { get; set; }
}
