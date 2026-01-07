using Genius.Api.InterfaceAdapters.DTO;
using Genius.Common.Lib.Results;

namespace Genius.Api.InterfaceAdapters.Abstractions
{
    public interface IAuthController
    {
        Task<Result<LoginResponse>> LoginAsync(LoginRequest request);
    }
}
