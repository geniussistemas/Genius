using Genius.QRCode.Api.App.Endpoints;
using Genius.QRCode.Api.Common;
using Genius.QRCode.Api.Infraestructure.Settings;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);
builder.AddLogging()
       .AddConfiguration()
       .AddArchitectures()
       .AddDataContexts()
       .AddCrossOrigin()
       .AddDocumentation()
       .AddServices()
       .AddContexts()
       .AddOptions();

var app = builder.Build();

var applicationSettings = app.Services.GetRequiredService<IOptions<ApplicationSettings>>().Value;

app.ConfigureEnvironment()
   .UseLogging()
   .UseArchitectures()
   .UseServices()
   .UseContexts()
   .MapEndpoints(applicationSettings)
   .UseCors(AppConstants.CorsPolicyName);

app.Run();


