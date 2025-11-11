using Genius.Application.Abstractions;
using Genius.Application.Abstractions.Caixa;
using Genius.Application.DTO;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;
using Genius.Domain.Exceptions;

namespace Genius.Application.UseCases.Caixa
{
    public class ObterDadosImpressaoTicketUseCase(IEstacionamentoRepository repository) : IObterDadosImpressaoTicketUseCase
    {
        public async Task<Result<DadosImpressaoTicket>> ExecutarAsync()
        {
            try
            {
                var estacionamento = await repository.GetDadosEstacionamentoAsync();

                var result = new DadosImpressaoTicket();

                ExtrairCabecalho(estacionamento, ref result);
                ExtrairRodape(estacionamento, ref result);

                return result;

            }
            catch (EstacionamentoNotFoundException)
            {
                return Error.NotFound("ESTACIONAMENTO.DADOS_NAO_ENCONTRADOS", "Não foi possível encontrar os dados do estacionamento.");

            }
            catch (Exception)
            {
                return Error.Failure("INTERNAL_SERVER_ERROR", "Ocorreu um erro interno ao processar a solicitação.");
            }
        }

        private static void ExtrairCabecalho(Estacionamento estac, ref DadosImpressaoTicket dados)
        {
            dados.Cabecalho[0] = string.IsNullOrWhiteSpace(estac.Nome) ? string.Empty : estac.Nome;


            var logradouroCompleto = string.Empty;

            if (estac.Endereco is not null)
            {
                if (!string.IsNullOrWhiteSpace(estac.Endereco.Logradouro))
                {
                    logradouroCompleto = estac.Endereco.Logradouro;
                }

                if (!string.IsNullOrWhiteSpace(estac.Endereco.Numero))
                {
                    if (logradouroCompleto.Length > 0)
                    {
                        logradouroCompleto += ", ";
                    }

                    logradouroCompleto += estac.Endereco.Numero;
                }

                if (!string.IsNullOrWhiteSpace(estac.Endereco.Bairro))
                {
                    logradouroCompleto += $" - {estac.Endereco.Bairro}";
                }
            }


            dados.Cabecalho[1] = string.IsNullOrWhiteSpace(logradouroCompleto) ? string.Empty : logradouroCompleto;

            dados.Cabecalho[2] = estac.Telefone is not null ? $"TEL: {estac.Telefone?.ToString() ?? string.Empty}" : string.Empty;

            dados.Cabecalho[3] = string.IsNullOrWhiteSpace(estac.Horario) ? string.Empty : estac.Horario;
        }

        private static void ExtrairRodape(Estacionamento estac, ref DadosImpressaoTicket dados)
        {
            if (string.IsNullOrWhiteSpace(estac.Dizeres))
            {
                dados.Rodape = null;
            }

            dados.Rodape = estac.Dizeres;
        }
    }
}
