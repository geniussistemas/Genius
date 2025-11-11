using Genius.Application.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Application.Abstractions.Caixa
{
    public interface IObterDadosImpressaoTicketUseCase
    {
        Task<Result<DadosImpressaoTicket>> ExecutarAsync();
    }
}
