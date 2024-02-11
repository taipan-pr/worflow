using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Application.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
