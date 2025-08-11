using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.Api.App.Extensions;

public static class AppArchitecturesExtension
{
    public static WebApplication UseArchitectures(this WebApplication app)
    {
        // Adiciona itens de arquitetura
        // Exemplo
        // app.MapControllers();

        return app;
    }

}