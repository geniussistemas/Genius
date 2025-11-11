using Genius.Api.App.Contexts.Common;
using Genius.Api.InterfaceAdapters.Abstractions;
using Genius.Api.InterfaceAdapters.DTO;
using Genius.Common.Api.DTO;

namespace Genius.Api.App.Contexts.CaixaContext
{
    public static class CaixaEndpoints
    {
        public static WebApplication MapCaixaEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/v1/caixas").WithTags("Caixas");

            group
                .MapPost(
                    "/",
                    async (CadastrarNovoCaixaRequest request, ICaixaController controller) =>
                    {
                        var result = await controller.CadastrarNovoCaixaAsync(request);
                        return ResultMapper.ToIResult(
                            result,
                            StatusCodes.Status201Created,
                            messageType: "CadastrarNovoCaixa",
                            code: "CAIXA_INCLUIDO"
                        );
                    }
                )
                .Produces<ApiResponse<CadastrarNovoCaixaResponse>>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status409Conflict)
                .Produces(StatusCodes.Status500InternalServerError)
                .WithSummary("Cria um novo caixa de cobrança")
                .WithDescription(
                    "Cadastra um novo caixa de cobrança no sistema do estacionamento. "
                        + "Cada caixa representa um ponto de atendimento utilizado para registrar veículos, processar pagamentos e realizar operações de controle. "
                        + "O número do terminal (informado no corpo da requisição) identifica fisicamente o caixa dentro da rede do estacionamento. "
                        + "Caso já exista um caixa com o mesmo número de terminal, será retornado o status **409 (Conflict)**. "
                        + "Em caso de falha na validação dos dados de entrada, será retornado **400 (Bad Request)**. "
                        + "Ao concluir o cadastro com sucesso, o caixa será retornado com o status **201 (Created)**."
                );

            group
                .MapGet(
                    "/{numeroTerminal:int}/configuracoes",
                    async (int numeroTerminal, ICaixaController controller) =>
                    {
                        var result = await controller.ObterConfiguracaoCaixaAsync(numeroTerminal);
                        return ResultMapper.ToIResult(
                            result,
                            messageType: "ObterConfigCaixa",
                            code: "CAIXA_CONF"
                        );
                    }
                )
                .Produces<ApiResponse<ConfiguracaoCaixaResponse>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError)
                .WithSummary("Obtém as configurações do caixa de cobrança")
                .WithDescription(
                    "Retorna as configurações associadas ao caixa especificado. "
                        + "As configurações incluem cabeçalhos de impressão, tabelas de preços, convênios e "
                        + "outros parâmetros necessários para o funcionamento do ponto de cobrança. "
                        + "Caso o identificador informado não corresponda a um caixa existente, "
                        + "será retornado o status **404 (Not Found)**."
                );

            return app;
        }
    }
}
