using Genius.Api.Application.Abstractions.Services;
using Genius.Api.Infraestructure.Services;
using Genius.Infraestructure.Frameworks.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Genius.Api.App.Extensions;

public static class BuilderServicesExtension
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // Infraestrutura
        builder.Services.AddSingleton<SerilogLoggerAdapter, SerilogLoggerAdapter>();

        builder
            .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["JwtSettings:SecretKey"]
                                ?? throw new InvalidOperationException("JwtSettings:SecretKey não configurada.")
                        )
                    ),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"]
                };
            });

        builder.Services.TryAddScoped<ITokenService, TokenService>();

        builder.Services.AddAuthorization();

        return builder;
    }
}
