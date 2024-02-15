using MediatR;
using Workflow.Application.Interfaces;

namespace Workflow.Application.Commands.Identity.DeleteCommand;

internal class DeleteCommandHandler : IRequestHandler<DeleteCommandRequest, DeleteCommandResponse>
{
    private readonly IIdentityProvider _identity;

    public DeleteCommandHandler(IIdentityProvider identity)
    {
        _identity = identity;
    }

    public async Task<DeleteCommandResponse> Handle(DeleteCommandRequest request, CancellationToken cancellationToken)
    {
        await _identity.DeleteAsync(request.Id, cancellationToken);
        return new DeleteCommandResponse();
    }
}
