using Workflow.Domain.DataTransferObjects.Identity;

namespace Workflow.Application.Interfaces;

public interface IIdentityProvider
{
    Task<Identity> RegisterAsync(CreateIdentity identity, CancellationToken cancellationToken = default);
    Task<Identity?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<Identity> SetEmailConfirmedAsync(string id, bool isConfirmed, CancellationToken cancellationToken = default);
    Task<Identity> SetDisabledAsync(string id, bool isDisabled, CancellationToken cancellationToken = default);
}
