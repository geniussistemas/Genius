namespace Genius.Application.Abstractions
{
    public interface IUseCase<in TRequest, TResponse>
    {
        Task<TResponse> ExecutarAsync(TRequest request);
    }
}
