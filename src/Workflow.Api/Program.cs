using Asp.Versioning;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Carter;
using Workflow.Api;
using Workflow.Api.Swagger;
using Workflow.Application.Extensions;
using Workflow.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Replace default container with Autofac container
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(e => e.RegisterModule<AutofacModule>())
    .ConfigureAppConfiguration((context, config) =>
    {
        var environment = context.HostingEnvironment.EnvironmentName;
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
        config.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
    });

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
});
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// By default ValidatorLifetime is Singleton and will append error message of one request after another
builder.Services.AddCarter(configurator: config => config.WithValidatorLifetime(ServiceLifetime.Scoped));

var app = builder.Build();
app.MapCarter();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseHttpsRedirection();

app.Run();
