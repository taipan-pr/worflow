using AutoMapper;
using FirebaseAdmin.Auth;
using Workflow.Application.Interfaces;
using Workflow.Domain.DataTransferObjects.Identity;

namespace Workflow.Infrastructure.Firebase;

internal class FirebaseIdentityProvider : IIdentityProvider
{
    private readonly FirebaseAuth _firebase;
    private readonly IMapper _mapper;

    public FirebaseIdentityProvider(FirebaseAuth firebase, IMapper mapper)
    {
        _firebase = firebase;
        _mapper = mapper;
    }

    public async Task<CreateIdentityResult> RegisterAsync(CreateIdentity identity, CancellationToken cancellationToken = default)
    {
        var args = new UserRecordArgs
        {
            Email = identity.Email,
            Password = identity.Password
        };

        var user = await _firebase.CreateUserAsync(args, cancellationToken);
        var result = _mapper.Map<CreateIdentityResult>(user);
        return result;
    }
}
