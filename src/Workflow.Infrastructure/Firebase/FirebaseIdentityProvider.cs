using AutoMapper;
using FirebaseAdmin.Auth;
using Workflow.Application.Exceptions;
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
        Identity? result = null;

        try
        {
            var args = new UserRecordArgs
            {
                Email = identity.Email,
                Password = identity.Password
            };

            var user = await _firebase.CreateUserAsync(args, cancellationToken);
            result = _mapper.Map<Identity>(user);
        }
        catch (FirebaseAuthException e)
        {
            ThrowCustomException(e);
        }

        return result!;
    }

    public async Task<Identity?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        Identity? result = null;
        try
        {
            var user = await _firebase.GetUserAsync(id, cancellationToken);
            result = _mapper.Map<Identity>(user);
            return result;
        }
        catch (FirebaseAuthException e)
        {
            ThrowCustomException(e);
        }

        return result;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            await _firebase.DeleteUserAsync(id, cancellationToken);
        }
        catch (FirebaseAuthException e)
        {
            ThrowCustomException(e);
        }
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

    private static void ThrowCustomException(FirebaseAuthException ex)
    {
        switch (ex.AuthErrorCode)
        {
            case AuthErrorCode.EmailAlreadyExists:
                throw new UserExistsException();
            case AuthErrorCode.UserNotFound:
                throw new UserNotFoundException();

            case AuthErrorCode.CertificateFetchFailed:
                break;
            case AuthErrorCode.ExpiredIdToken:
                break;
            case AuthErrorCode.InvalidIdToken:
                break;
            case AuthErrorCode.PhoneNumberAlreadyExists:
                break;
            case AuthErrorCode.UidAlreadyExists:
                break;
            case AuthErrorCode.UnexpectedResponse:
                break;
            case AuthErrorCode.InvalidDynamicLinkDomain:
                break;
            case AuthErrorCode.RevokedIdToken:
                break;
            case AuthErrorCode.InvalidSessionCookie:
                break;
            case AuthErrorCode.ExpiredSessionCookie:
                break;
            case AuthErrorCode.RevokedSessionCookie:
                break;
            case AuthErrorCode.ConfigurationNotFound:
                break;
            case AuthErrorCode.TenantNotFound:
                break;
            case AuthErrorCode.TenantIdMismatch:
                break;
            case AuthErrorCode.EmailNotFound:
                break;

            // Do nothing about these exceptions
            case null:
            default:
                break;
        }
    }
}
