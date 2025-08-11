using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Genius.Api.Data;

namespace Genius.Api.App.Extensions;

public static class BuilderDataContextExtension
{
    public static WebApplicationBuilder AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDbContext<AppDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.ConnectionString);
                });

        return builder;
    }
}
