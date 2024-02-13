using System.Reflection;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Workflow.Application.PipelineBehaviors;
using Module = Autofac.Module;

namespace Workflow.Application;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

        builder.RegisterAutoMapper(assembly);

        var configuration = MediatRConfigurationBuilder
            .Create(assembly)
            .WithAllOpenGenericHandlerTypesRegistered()
            .WithRegistrationScope(RegistrationScope.Scoped)
            .WithCustomPipelineBehaviors(new[]
            {
                typeof(ValidationBehavior<,>)
            })
            .Build();
        builder.RegisterMediatR(configuration);
    }
}
