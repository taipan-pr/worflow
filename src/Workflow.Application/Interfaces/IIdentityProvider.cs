using Workflow.Domain.DataTransferObjects.Identity;

namespace Workflow.Application.Interfaces;

public interface IIdentityProvider
{
    Task<CreateIdentityResult> RegisterAsync(CreateIdentity identity, CancellationToken cancellationToken = default);
}
