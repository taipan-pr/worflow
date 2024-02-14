using AutoMapper;
using MediatR;
using Workflow.Application.Interfaces;

namespace Workflow.Application.Queries.Identity.GetIdentityByIdQuery;

internal class GetIdentityByIdQueryHandler : IRequestHandler<GetIdentityByIdQueryRequest, GetIdentityByIdQueryResponse?>
{
    private readonly IIdentityProvider _identity;
    private readonly IMapper _mapper;

    public GetIdentityByIdQueryHandler(IIdentityProvider identity, IMapper mapper)
    {
        _identity = identity;
        _mapper = mapper;
    }

    public async Task<GetIdentityByIdQueryResponse?> Handle(GetIdentityByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var result = await _identity.FindByIdAsync(request.Id!, cancellationToken);

        return result is null ? null : _mapper.Map<GetIdentityByIdQueryResponse>(result);
    }
}
