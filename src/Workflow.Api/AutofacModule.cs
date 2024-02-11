using System.Reflection;
using Autofac;
using Carter;
using FluentValidation;
using Module = Autofac.Module;

namespace Workflow.Api;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly)
            .Where(e =>
            {
                // Since Carter will register all of its dependencies (ICarterModule and IValidator) from AddCarter() we don't need to add that in via Autofac
                var isAllowedInterfaces = e.GetInterfaces().Any(f => f == typeof(ICarterModule) || f == typeof(IValidator));
                return !isAllowedInterfaces;
            })
            .AsImplementedInterfaces();

        builder.RegisterModule<Infrastructure.AutofacModule>();
    }
}
