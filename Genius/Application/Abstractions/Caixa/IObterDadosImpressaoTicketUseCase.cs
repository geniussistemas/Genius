using Genius.Application.DTOs;
using Genius.Common.Lib.Results;

namespace Genius.Application.Abstractions.Caixa
{
    public interface IObterDadosImpressaoTicketUseCase
    {
        Task<Result<DadosImpressaoTicket>> ExecutarAsync();
    }
}
