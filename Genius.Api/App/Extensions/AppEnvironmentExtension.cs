using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.Api.App.Extensions;

public static class AppEnvironmentExtension
{
    public static WebApplication ConfigureEnvironment(this WebApplication app)
    {
        // Documentação da API (Swagger) apenas em desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            // Usado para quando há autenticação
            // app.MapSwagger().RequireAuthorization();
        }

        return app;
    }

}