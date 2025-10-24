using Genius.Application.Abstractions;
using Genius.Common.Lib.Results;
using Genius.Domain.Entities;

namespace Genius.Application.UseCases.Caixa
{
    public interface ICreateCaixaUseCase : IUseCase<TerminalCaixa, Result<TerminalCaixa>>
    {
    }
}
