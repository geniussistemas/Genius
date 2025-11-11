using Genius.Api.App.Endpoints;
using Genius.Api.Common;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddLogging()
    .AddConfiguration()
    .AddArchitectures()
    .AddDataContexts()
    .AddCrossOrigin()
    .AddDocumentation()
    .AddServices()
    .AddContexts();

var app = builder.Build();

app.ConfigureEnvironment()
    .UseLogging()
    .UseArchitectures()
    .UseServices()
    .UseContexts()
    .MapEndpoints()
    .UseCors(AppConstants.CorsPolicyName);

await app.RunAsync();
