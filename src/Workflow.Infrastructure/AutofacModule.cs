using System.Reflection;
using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Module = Autofac.Module;

namespace Workflow.Infrastructure;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces();

        builder.RegisterAutoMapper(assembly);

        builder.Register(e => FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile("../../workflow-firebase-adminsdk.json")
        })).SingleInstance().AsSelf();

        builder.Register(e =>
        {
            var firebaseApp = e.Resolve<FirebaseApp>();
            return FirebaseAuth.GetAuth(firebaseApp);
        }).SingleInstance().AsSelf();

        builder.RegisterModule<Application.AutofacModule>();
    }
}
