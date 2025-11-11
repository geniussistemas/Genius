using Genius.Application.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Application.Abstractions.Convenio
{
    public interface IObterConveniosSimplificadoUseCase
    {
        Task<Result<List<ConvenioSimplificado>>> ExecutarAsync();
    }
}
