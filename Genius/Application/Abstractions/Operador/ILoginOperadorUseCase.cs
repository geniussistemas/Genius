using Genius.Application.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Application.Abstractions.Operador
{
    public interface ILoginOperadorUseCase
    {
        Task<Result<LoginOperadorOutput>> ExecutarAsync(LoginOperadorInput loginInfo);
    }
}
