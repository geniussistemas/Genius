using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.Controllers;
using Genius.Application.Abstractions.Operador;
using Genius.Application.UseCases.Operador;
using Genius.Infraestructure.Persistence;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Genius.Api.App.Contexts.OperadorContext
{
    public static class OperadorExtensions
    {
        public static WebApplicationBuilder AddOperadorContext(this WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<IOperadorController, OperadorController>();
            builder.Services.TryAddScoped<IOperadorRepository, OperadorRepository<AppDbContext>>();
            builder.Services.TryAddScoped<IObterListaUsuariosUseCase, ObterListaUsuariosUseCase>();


            return builder;
        }


        public static WebApplication UseOperadorContext(this WebApplication app)
        {
            app.MapOperadorEndpoints();
            return app;
        }
    }
}
