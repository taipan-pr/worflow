using MediatR;

namespace Workflow.Application.Commands.Identity.RegisterCommand;

public record RegisterCommandRequest : IRequest<RegisterCommandResponse>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
