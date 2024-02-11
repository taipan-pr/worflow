using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Workflow.Infrastructure;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

        builder.RegisterModule<Application.AutofacModule>();
    }
}
