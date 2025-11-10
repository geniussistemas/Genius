using Genius.Application.Abstractions.Caixa;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;

namespace Genius.Application.UseCases.Caixa
{
    public class CreateCaixaUseCase(ITerminalCaixaRepository repository) : ICreateCaixaUseCase
    {
        public async Task<Result<TerminalCaixa>> ExecutarAsync(TerminalCaixa novoCaixa)
        {
            try
            {
                if (await repository.NumeroTerminalExisteAsync(novoCaixa.Terminal))
                {
                    return Error.Conflict("CAIXA.TERMINAL_DUPLICADO", "Já existe um caixa registrado com o número de terminal informado.");
                }

                if (await repository.NomeTerminalExisteAsync(novoCaixa.Nome!))
                {
                    return Error.Conflict("CAIXA.NOME_DUPLICADO", "Já existe um caixa registrado com nome informado.");
                }

                novoCaixa = ConfiguraValoresPadrao(novoCaixa);

                var terminalCadastrado = await repository.AdicionarAsync(novoCaixa);

                return terminalCadastrado;
            }
            catch (Exception)
            {
                return Error.Failure("INTERNAL_SERVER_ERROR", "Ocorreu um erro interno ao processar a solicitação.");
            }
        }


        private static TerminalCaixa ConfiguraValoresPadrao(TerminalCaixa caixa)
        {
            caixa.Ativo = true;
            caixa.Vinculado = true;
            caixa.DataInclusao = DateTime.Now;
            caixa.DataAlteracao = null;

            return caixa;
        }
    }
}
