using Genius.Common.Lib.Results;
using Genius.Domain.Entities;

namespace Genius.Application.Abstractions.Caixa
{
    public interface ICreateCaixaUseCase : IUseCase<TerminalCaixa, Result<TerminalCaixa>>;
}
