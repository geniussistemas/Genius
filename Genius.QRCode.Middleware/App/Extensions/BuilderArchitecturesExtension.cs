using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.QRCode.Middleware.App.Extensions;

public static class BuilderArchitecturesExtension
{
    public static WebApplicationBuilder AddArchitectures(this WebApplicationBuilder builder)
    {
        // Adiciona serviços de arquitetura
        // Exemplo
        // builder.Services.AddControllers();
        
        return builder;
    }
}