using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Exceptions;
using Workflow.Application.Options;

namespace Workflow.Api.Extensions;

internal static class HostBuilderExtensions
{
    public static IHostBuilder AddAutofac(this IHostBuilder host)
    {
        host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(e => e.RegisterModule<AutofacModule>());

        return host;
    }

    public static IHostBuilder AddConfiguration(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration((context, config) =>
        {
            var environment = context.HostingEnvironment.EnvironmentName;
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
            config.AddEnvironmentVariables();
            config.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
        });

        return host;
    }

    public static IHostBuilder AddSerilog(this IHostBuilder host)
    {
        host.UseSerilog((context, services, config) =>
        {
            var seqOptions = services.GetRequiredService<IOptions<SeqOptions>>();
            if(!string.IsNullOrEmpty(seqOptions.Value.Host) && !string.IsNullOrEmpty(seqOptions.Value.ApiKey))
            {
                config.WriteTo.Seq(seqOptions.Value.Host, apiKey: seqOptions.Value.ApiKey);
            }

            config
#if DEBUG
                .Enrich.WithProperty("Environment", "Local")
#else
                .Enrich.WithProperty("Environment", context.Configuration["ASPNETCORE_ENVIRONMENT"])
#endif
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .Enrich.WithClientIp()
                .Enrich.WithCorrelationId(addValueIfHeaderAbsence: true)
                .Enrich.WithExceptionDetails()
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services);
        });

        return host;
    }
}
