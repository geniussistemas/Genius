using Genius.Api;
using Genius.Api.Common;
using Genius.Api.Contexts.TicketContext;
using Genius.Api.Endpoints;
//using Genius.Api.App.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddLogging()
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

app.Run();
