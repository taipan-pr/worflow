using MediatR;

namespace Workflow.Application.Commands.RegisterCommand;

public record RegisterCommandRequest : IRequest<RegisterCommandResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
