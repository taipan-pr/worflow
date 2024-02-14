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

    public async Task<Identity> RegisterAsync(CreateIdentity identity, CancellationToken cancellationToken = default)
    {
        var args = new UserRecordArgs
        {
            Email = identity.Email,
            Password = identity.Password
        };

        var user = await _firebase.CreateUserAsync(args, cancellationToken);
        var result = _mapper.Map<Identity>(user);
        return result;
    }

    public async Task<Identity> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var user = await _firebase.GetUserAsync(id, cancellationToken);
        var result = _mapper.Map<Identity>(user);
        return result;
    }

    public async Task<Identity> SetEmailConfirmedAsync(string id, bool isConfirmed, CancellationToken cancellationToken = default)
    {
        var args = new UserRecordArgs
        {
            Uid = id,
            EmailVerified = isConfirmed
        };

        var user = await _firebase.UpdateUserAsync(args, cancellationToken);
        var result = _mapper.Map<Identity>(user);
        return result;
    }

    public async Task<Identity> SetDisabledAsync(string id, bool isDisabled, CancellationToken cancellationToken = default)
    {
        var args = new UserRecordArgs
        {
            Uid = id,
            Disabled = isDisabled
        };

        var user = await _firebase.UpdateUserAsync(args, cancellationToken);
        var result = _mapper.Map<Identity>(user);
        return result;
    }
}
