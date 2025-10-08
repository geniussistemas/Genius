using Genius.QRCode.Middleware;
using Genius.QRCode.Middleware.Common;
using Genius.QRCode.Middleware.Contexts.TicketContext;
using Genius.QRCode.Middleware.Endpoints;
//using Genius.QRCode.Middleware.App.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddLogging()
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

app.Run();
