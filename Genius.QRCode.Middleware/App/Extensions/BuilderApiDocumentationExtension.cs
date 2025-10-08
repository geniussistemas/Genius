using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genius.QRCode.Middleware.App.Extensions;

public static class BuilderApiDocumentationExtension
{
    public static WebApplicationBuilder AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            // Garante que o Swagger usará os namespaces completos
            // No lugar de "Category", ele usará "Fina.Core.Models.Category"
            // Ajuda quando há classes com mesmo nome em namespaces diferentes
            x.CustomSchemaIds(n => n.FullName);
        });

        return builder;
    }
    
}