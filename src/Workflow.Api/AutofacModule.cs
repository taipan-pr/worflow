using Autofac;

namespace Workflow.Api;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterModule<Infrastructure.AutofacModule>();
    }
}
