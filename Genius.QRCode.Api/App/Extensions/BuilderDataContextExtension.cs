using Genius.QRCode.Api.App.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Genius.QRCode.Api.App.Extensions;

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
