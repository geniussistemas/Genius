using Microsoft.EntityFrameworkCore;
using Genius.QRCode.Middleware.Infraestructure.Persistence;

namespace Genius.QRCode.Middleware.App.Extensions;

public static class BuilderDataContextExtension
{
    public static WebApplicationBuilder AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext<AppDbContext>(
                options =>
                {
                    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connectionString);
                });

        return builder;
    }
}
