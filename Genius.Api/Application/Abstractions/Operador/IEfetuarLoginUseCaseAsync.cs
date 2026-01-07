using Genius.Api.Application.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Api.Application.Abstractions.Operador
{
    public interface IEfetuarLoginUseCaseAsync
    {
        Task<Result<LoginOperador>> ExecutarAsync(LoginInput loginInfo);
    }
}
