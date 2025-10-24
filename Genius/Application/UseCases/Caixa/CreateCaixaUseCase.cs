using Genius.Application.Abstractions;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;

namespace Genius.Application.UseCases.Caixa
{
    public class CreateCaixaUseCase(ITerminalCaixaRepository repository) : ICreateCaixaUseCase
    {
        public async Task<Result<TerminalCaixa>> ExecutarAsync(TerminalCaixa request)
        {
            try
            {
                if (await repository.NumeroTerminalExisteAsync(request.Terminal))
                {
                    return Error.Conflict("CAIXA.TERMINAL_DUPLICADO", "Já existe um caixa registrado com o número de terminal informado.");
                }

                if (await repository.NomeTerminalExisteAsync(request.Nome!))
                {
                    return Error.Conflict("CAIXA.NOME_DUPLICADO", "Já existe um caixa registrado com nome informado.");
                }

                request = ConfiguraValoresPadrao(request);

                var terminalCadastrado = await repository.AdicionarAsync(request);

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
