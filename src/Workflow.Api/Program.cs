using Autofac;
using Autofac.Extensions.DependencyInjection;
using Carter;
using Workflow.Api;
using Workflow.Application.Extensions;
using Workflow.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Replace default container with Autofac container
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(e => e.RegisterModule<AutofacModule>());

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

// By default ValidatorLifetime is Singleton and will append error message of one request after another
builder.Services.AddCarter(configurator: config => config.WithValidatorLifetime(ServiceLifetime.Scoped));

var app = builder.Build();
app.MapCarter();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
