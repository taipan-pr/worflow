using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Options;

namespace Workflow.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddOptions<SeqOptions>().BindConfiguration("Seq");

        return services;
    }
}
