using Genius.Api.App.Contexts.Common;
using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.DTO;
using Genius.Common.Api.DTO;

namespace Genius.Api.App.Contexts.OperadorContext
{
    public static class OperadorEndpoints
    {
        public static WebApplication MapOperadorEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/v1/operadores").WithTags("Operadores");

            group
                .MapGet(
                    "/{numeroTerminal:int}/usuarios",
                    async (int numeroTerminal, IOperadorController controller) =>
                    {
                        var result = await controller.ObterTodosUsuariosAsync(numeroTerminal);
                        return ResultMapper.ToIResult(result, messageType: "ObterUsuarios", code: "OBTER_USUARIOS");
                    }
                )
                .Produces<ApiRequest<ObterUsuarioResponse>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError)
                .WithSummary("Obtém todos os usuários ativos no sistema.")
                .WithDescription(
                    "Obtém a lista de usuários ativos vinculados a um terminal específico. "
                        + "O numero do terminal é identificado pelo número informado na rota da requisição. "
                        + "Somente terminais previamente cadastrados no sistema poderão retornar usuários. "
                        + "Caso o numero terminal não exista, será retornado o status **404 (Not Found).**. "
                        + "E caso o número do terminal seja inválido, será retornado o status **400 (Bad Request).**. "
                        + "Em caso de erro interno durante a consulta, será retornado **500 (Internal Server Error)**. "
                        + "Quando a operação for bem-sucedida, será retornada a lista de usuários com o status **200 (OK)**."
                );

            return app;
        }
    }
}
