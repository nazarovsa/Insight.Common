using System;
using Marten.Exceptions;
using Npgsql;

namespace Insight.Marten.Extensions
{
	public static class ExceptionExtensions
	{
		private const string PostgresDuplicateKeyErrorCode = "23505";

		public static bool IsDuplicateKeyException(this MartenCommandException ex) =>
			ex.InnerException != null && ex.InnerException is PostgresException postgresException &&
			postgresException.SqlState.Equals(PostgresDuplicateKeyErrorCode,
				StringComparison.InvariantCultureIgnoreCase);
	}
}