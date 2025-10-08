namespace Genius.Api.Common.Results
{
    /// <summary>
    /// Representa o resultado de uma operação que pode falhar
    /// </summary>
    public class Result<T>
    {
        private readonly T? _value;

        private readonly List<Error> _errors;

        private Result(T value)
        {
            _value = value;
            _errors = [];
            IsSuccess = true;
        }

        private Result(List<Error> errors)
        {
            _errors = errors;
            _value = default;
            IsSuccess = false;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Não é possível acessar o valor de um resultado com falha.");

        public IReadOnlyList<Error> Errors => _errors.AsReadOnly();

        public Error FirstError => _errors[0];

        public static Result<T> Success(T value) => new(value);

        public static Result<T> Failure(Error error) => new([error]);

        public static Result<T> Failure(List<Error> errors) => new(errors);

        // Implicit conversions
        public static implicit operator Result<T>(T value) => Success(value);
        public static implicit operator Result<T>(Error error) => Failure(error);
        public static implicit operator Result<T>(List<Error> errors) => Failure(errors);
    }
}
