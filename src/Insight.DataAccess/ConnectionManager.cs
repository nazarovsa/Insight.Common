using System;
using System.Collections.Generic;
using System.Data;

namespace Insight.DataAccess
{
	/// <summary>
	/// Connection manager
	/// </summary>
	public abstract class ConnectionManager : IConnectionManager
	{
		protected readonly ConnectionOptions ConnectionOptions;

		private readonly Stack<ITransactionScope> _scopes =
			new Stack<ITransactionScope>();

		private IDbConnection _connection;

		private IDbTransaction _transaction;

		/// <summary>
		/// Connection manager
		/// </summary>
		/// <param name="connectionOptions">Connection options</param>
		protected ConnectionManager(ConnectionOptions connectionOptions)
		{
			if (connectionOptions == null)
				throw new ArgumentNullException(nameof(connectionOptions));

			if (string.IsNullOrWhiteSpace(connectionOptions.ConnectionString))
				throw new ArgumentException(nameof(connectionOptions));

			ConnectionOptions = connectionOptions;
		}

		/// <inheritdoc />
		public IDbConnection GetConnection()
		{
			EnsureConnectionCreated();

			return _connection;
		}

		/// <inheritdoc />
		public IDbTransaction GetTransaction()
		{
			return _transaction;
		}

		/// <inheritdoc />
		public IDisposable BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
		{
			EnsureConnectionCreated();

			if (_transaction == null)
			{
				_transaction = _connection.BeginTransaction(level);

				var scope = new ParentTransactionScope(this);

				_scopes.Push(scope);

				return scope;
			}
			else
			{
				var scope = new NestedTransactionScope(this);

				_scopes.Push(scope);

				return scope;
			}
		}

		/// <inheritdoc />
		public void CommitTransaction()
		{
			if (_scopes.TryPop(out var scope))
				scope.Commit();
		}

		/// <inheritdoc />
		public void RollbackTransaction()
		{
			if (_scopes.TryPop(out var scope))
				scope.Dispose();
		}

		public void Dispose()
		{
			_connection?.Dispose();
			_connection = null;
			_transaction?.Dispose();
			_transaction = null;
		}

		protected abstract IDbConnection CreateConnection();
		
		private void EnsureConnectionCreated()
		{
			if (_connection == null)
			{
				_connection = CreateConnection();
				_connection.Open();
			}
		}

		private interface ITransactionScope : IDisposable
		{
			void Commit();
		}

		private class ParentTransactionScope : ITransactionScope
		{
			private readonly ConnectionManager _manager;

			public ParentTransactionScope(ConnectionManager manager)
			{
				_manager = manager;
			}

			public void Commit()
			{
				if (_manager._transaction != null)
				{
					_manager._transaction.Commit();
					_manager._transaction.Dispose();
					_manager._transaction = null;
				}
			}

			public void Dispose()
			{
				if (_manager._transaction != null)
				{
					if (_manager._connection != null && _manager._connection.State == ConnectionState.Open)
						_manager._transaction.Rollback();

					_manager._transaction.Dispose();
					_manager._transaction = null;
				}
			}
		}

		private class NestedTransactionScope : ITransactionScope
		{
			private readonly ConnectionManager _manager;

			private bool _committed;

			public NestedTransactionScope(ConnectionManager manager)
			{
				_manager = manager;
			}

			public void Dispose()
			{
				if (!_committed)
				{
					if (_manager._transaction != null)
					{
						if (_manager._connection != null && _manager._connection.State == ConnectionState.Open)
							_manager._transaction.Rollback();

						_manager._transaction.Dispose();
						_manager._transaction = null;
					}
				}
			}

			public void Commit()
			{
				_committed = true;
			}
		}
	}
}