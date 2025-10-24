using Genius.Application.Abstractions;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;

namespace Genius.Application.UseCases.Caixa
{
    public interface ICreateCaixa : IUseCase<TerminalCaixa, Result<TerminalCaixa>>
    {
    }
}
