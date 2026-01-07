using Genius.Api.App.Contexts.Common;
using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.DTO;
using Genius.Common.Api.DTO;


namespace Genius.Api.App.Contexts.AuthContext
{
    public static class AuthEndpoints
    {
        public static WebApplication MapAuthEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/v1/auth").WithTags("Autenticação");

            group.MapPost("/login", async (LoginRequest request, IAuthController controller) =>
            {
                var result = await controller.LoginAsync(request);
                return ResultMapper.ToIResult(result);
            }
            )
            .Produces<ApiResponse<LoginResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithSummary("Realiza a autenticação de um operador no sistema.")
            .WithDescription(
                "Realiza o processo de autenticação de um operador a partir " +
                "das credenciais informadas no corpo da requisição. " +
                "O corpo deve conter o login e a senha do operador, conforme o modelo **LoginRequest**. " +
                "Se as credenciais forem válidas, será retornado o token de autenticação e " +
                "as informações básicas do usuário com o status **200 (OK)**. " +
                "Caso o login não exista, será retornado **404 (Not Found)**. " +
                "Se as credenciais forem inválidas, será retornado **401 (Unauthorized)**. " +
                "Em caso de erro interno no processamento da requisição, " +
                "será retornado **500 (Internal Server Error)**.");



            return app;
        }

    }
}
