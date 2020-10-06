namespace Insight.Result
{
	public class Result
	{
		protected Result()
		{
		}

		public bool Success { get; protected set; }

		public string Message { get; protected set; }

		public Result Ok() => new Result {Success = true};

		public Result Fail(string message) => new Result {Message = message};

		public static Result<T> Ok<T>(T value) => Result<T>.Ok(value);

		public static Result<T> Fail<T>(string message) => Result<T>.Fail(message);
	}

	public sealed class Result<T> : Result
	{
		private Result()
		{
		}

		public T Value { get; private set; }

		public static Result<T> Ok(T value) => new Result<T> {Success = true, Value = value};

		public new static Result<T> Fail(string message) => new Result<T> {Message = message};
	}
}