using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Insight.DataAccess.Dapper
{
	public static class DapperExtensions
	{
		public static Task<IEnumerable<dynamic>> QueryAsync(
			this IConnectionManager manager, string query, CancellationToken cancellationToken = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, null, cancellationToken);

			return connection.QueryAsync(definition);
		}

		public static Task<IEnumerable<dynamic>> QueryAsync(
			this IConnectionManager manager, string query, object parameters,
			CancellationToken cancellationToken = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, parameters, cancellationToken);

			return connection.QueryAsync(definition);
		}

		public static Task<IEnumerable<T>> QueryAsync<T>(
			this IConnectionManager manager, string query, object parameters,
			CancellationToken cancellationToken = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, parameters, cancellationToken);

			return connection.QueryAsync<T>(definition);
		}

		public static Task<dynamic> QueryFirstOrDefaultAsync(
			this IConnectionManager manager, string query, object parameters,
			CancellationToken cancellationToken = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, parameters, cancellationToken);

			return connection.QueryFirstOrDefaultAsync(definition);
		}

		public static Task<T> QueryFirstOrDefaultAsync<T>(
			this IConnectionManager manager, string query, object parameters,
			CancellationToken cancellationToken = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, parameters, cancellationToken);

			return connection.QueryFirstOrDefaultAsync<T>(definition);
		}

		public static Task<T> QuerySingleOrDefaultAsync<T>(
			this IConnectionManager manager, string query, object parameters,
			CancellationToken cancellationToken = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, parameters, cancellationToken);

			return connection.QuerySingleOrDefaultAsync<T>(definition);
		}

		public static Task<T> QuerySingleAsync<T>(
			this IConnectionManager manager, string query, object parameters,
			CancellationToken cancellationToken = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, parameters, cancellationToken);

			return connection.QuerySingleAsync<T>(definition);
		}

		public static int Execute(
			this IConnectionManager manager, string query, object parameters,
			CancellationToken cancellationToken = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, parameters, cancellationToken);

			return connection.Execute(definition);
		}

		public static Task<int> ExecuteAsync(
			this IConnectionManager manager, string query, object parameters,
			CancellationToken cancellationToken = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, parameters, cancellationToken);

			return connection.ExecuteAsync(definition);
		}

		public static Task<T> ExecuteScalarAsync<T>(
			this IConnectionManager manager, string query, object parameters = null, CancellationToken token = default)
		{
			var connection = manager.GetConnection();
			var definition = manager.GetDefinition(query, parameters, token);

			return connection.ExecuteScalarAsync<T>(definition);
		}

		private static CommandDefinition GetDefinition(
			this IConnectionManager manager, string query, object parameters, CancellationToken token = default)
		{
			return new CommandDefinition(query, parameters, manager.GetTransaction(), cancellationToken: token);
		}
	}
}