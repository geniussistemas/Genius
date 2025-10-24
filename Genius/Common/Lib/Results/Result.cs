namespace Genius.Common.Lib.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Error> Errors { get; }

        protected Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors ?? [];
        }

        public static Result Success() => new(true, []);
        public static Result<T> Success<T>(T value) => new(value, true, []);

        public static Result Failure(Error error) => new(false, [error]);
        public static Result Failure(IEnumerable<Error> errors) => new(false, [.. errors]);
        public static Result<T> Failure<T>(Error error) => new(default!, false, [error]);
        public static Result<T> Failure<T>(IEnumerable<Error> errors) => new(default!, false, [.. errors]);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        internal Result(T? value, bool isSuccess, IReadOnlyList<Error> errors) : base(isSuccess, errors)
        {
            Value = value;
        }

        public static implicit operator Result<T>(T value) => Success(value);

        public static implicit operator Result<T>(Error error) => Failure<T>(error);

        public static implicit operator Result<T>(Error[] errors) => Failure<T>(errors);
    }
}
