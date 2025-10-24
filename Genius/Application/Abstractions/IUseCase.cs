namespace Genius.Application.Abstractions
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<TResponse> ExecutarAsync(TRequest request);
    }
}
