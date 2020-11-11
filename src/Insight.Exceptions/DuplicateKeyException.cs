using System;

namespace Insight.Exceptions
{
	public sealed class DuplicateKeyException : Exception
	{
		public DuplicateKeyException(string message) : base(message)
		{
		}

		public DuplicateKeyException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}