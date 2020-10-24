using System;

namespace Insight.Dto
{
	public class CustomError
	{
		public string Message { get; private set; }

		public Exception Exception { get; private set; }

		public CustomError(string message)
		{
			Message = message;
		}

		public CustomError(string message, Exception ex) : this(message)
		{
			Exception = ex;
		}

		public static CustomError Create(string message)
		{
			return new CustomError(message);
		}

		public static CustomError Create(string message, Exception ex)
		{
			return new CustomError(message, ex);
		}
	}
}