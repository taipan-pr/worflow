using System.Reflection;
using Autofac;
using Carter;
using Microsoft.AspNetCore.Diagnostics;
using Module = Autofac.Module;

namespace Workflow.Api;

internal class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly)
            .Where(e =>
            {
                // Since Carter will register all of its dependencies (ICarterModule) from AddCarter() we don't need to add that in via Autofac
                // All exception handlers will be added in Program.cs
                var isAllowedInterfaces = e.GetInterfaces().Any(f => f == typeof(ICarterModule) || f == typeof(IExceptionHandler));
                return !isAllowedInterfaces;
            })
            .AsImplementedInterfaces();

        builder.RegisterModule<Infrastructure.AutofacModule>();
    }
}
