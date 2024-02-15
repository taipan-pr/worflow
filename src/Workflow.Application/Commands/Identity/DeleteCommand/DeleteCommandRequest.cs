using MediatR;

namespace Workflow.Application.Commands.Identity.DeleteCommand;

public record DeleteCommandRequest : IRequest<DeleteCommandResponse>
{
    public required string Id { get; set; }
}
