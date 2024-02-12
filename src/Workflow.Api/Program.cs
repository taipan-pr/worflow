using Carter;
using Workflow.Api.ExceptionHandlers;
using Workflow.Api.Extensions;
using Workflow.Api.Middlewares;
using Workflow.Application.Extensions;
using Workflow.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .AddAutofac()
    .AddConfiguration()
    .AddSerilog();

// Add services to the container.
builder.Services
    .AddHttpContextAccessor()
    .AddEndpointsApiExplorer()
    .AddSwagger()
    .AddInfrastructure()
    .AddApplication()
    .AddVersioning()
    // By default ValidatorLifetime is Singleton and will append error message of one request after another
    .AddCarter(configurator: config => config.WithValidatorLifetime(ServiceLifetime.Scoped));

// Setup global exception handler
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<UnhandledExceptionHandler>();

var app = builder.Build();
app.MapCarter();

if(app.Environment.IsDevelopment())
{
    app.UseSwaggerInterface();
}

app.UseMiddleware<TimerMiddleware>();

app.UseMiddleware<LoggerMiddleware>();

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.Run();
