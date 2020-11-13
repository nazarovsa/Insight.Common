namespace Insight.Dto
{
	public record Result(bool Success, string Message)
	{
		public static Result Ok() => new Result(true, default);
		public static Result Fail(string message) => new Result(false, message);
		public static Result<T> Ok<T>(T value) => Result<T>.Ok(value);
		public static Result<T> Fail<T>(string message) => Result<T>.Fail(message);

		public void Deconstruct(out bool success)
			=> success = Success;

		public void Deconstruct(out bool success, out string message)
		{
			success = Success;
			message = Message;
		}
	}

	public sealed record Result<T>(bool Success, string Message, T Value) : Result(Success, Message)
	{
		public static Result<T> Ok(T value) => new Result<T>(true, default, value);

		public new static Result<T> Fail(string message) => new Result<T>(default, message, default);

		public void Deconstruct(out bool success, out string message, out T value)
		{
			success = Success;
			message = Message;
			value = Value;
		}
	}
}