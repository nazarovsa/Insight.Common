using System;
using System.Transactions;

namespace Insight.Transactions
{
	public sealed class AsyncTransactionScope : IDisposable
	{
		private TransactionScope _scope;

		public bool Disposed { get; private set; }

		public AsyncTransactionScope()
		{
			_scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
			{
				IsolationLevel = IsolationLevel.ReadCommitted
			}, TransactionScopeAsyncFlowOption.Enabled);
		}

		public AsyncTransactionScope(IsolationLevel isolationLevel) : this(TransactionScopeOption.Required,
			isolationLevel)
		{
		}

		public AsyncTransactionScope(TransactionScopeOption scopeOption,
			IsolationLevel isolationLevel)
		{
			_scope = new TransactionScope(scopeOption, new TransactionOptions
			{
				IsolationLevel = isolationLevel,
			}, TransactionScopeAsyncFlowOption.Enabled);
		}


		public void Complete()
		{
			_scope.Complete();
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!Disposed)
			{
				if (disposing)
				{
					_scope.Dispose();
				}

				Disposed = true;
			}
		}

		~AsyncTransactionScope()
		{
			Dispose(false);
		}
	}
}