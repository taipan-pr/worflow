using AutoMapper;
using MediatR;
using Workflow.Application.Interfaces;
using Workflow.Domain.DataTransferObjects.Identity;

namespace Workflow.Application.Commands.RegisterCommand;

internal class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
{
    private readonly IIdentityProvider _identity;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IIdentityProvider identity, IMapper mapper)
    {
        _identity = identity;
        _mapper = mapper;
    }

    public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        throw new ArgumentException("Random");
        var model = _mapper.Map<CreateIdentity>(request);
        var registerResult = await _identity.RegisterAsync(model, cancellationToken);
        var response = _mapper.Map<RegisterCommandResponse>(registerResult);
        return response;
    }
}
