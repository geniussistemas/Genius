using Genius.QRCode.Api;
using Genius.QRCode.Api.App.Endpoints;
using Genius.QRCode.Api.Common;
using Genius.QRCode.Api.Contexts.TicketContext;
//using Genius.QRCode.Api.App.Extensions;

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
